using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XAlarm.Center.Domain.Events;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Messages.Lines;
using XAlarm.Center.Domain.Messages.Telegrams;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Projects;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;
using XAlarm.Center.Shared.Helpers;
using TextMessage = XAlarm.Center.Domain.Messages.Telegrams.TextMessage;

// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataQuery

namespace XAlarm.Center.Service;

internal sealed class AlarmService(ILogger<AlarmService> logger, ApplicationDbContext dbContext, HttpClient httpClient)
    : IAlarmService
{
    public async Task<MessageEvent> SendMessageAsync(AlarmPayload alarmPayload)
    {
        var globalSetting = await dbContext.GlobalSettings.AsNoTracking().SingleOrDefaultAsync();

        if (globalSetting is null)
            return new MessageEvent
            {
                IsSuccess = false, Type = (int)EventTypes.Error,
                TypeDescription = EventTypes.EmailSent.GetDescription(), MessageBegin = "Invalid system setting"
            };

        var project = await dbContext.Projects.AsNoTracking()
            .SingleOrDefaultAsync(x => x.ProjectId == alarmPayload.ProjectId);

        if (project is null)
            return new MessageEvent
            {
                IsSuccess = false, Type = (int)EventTypes.Error,
                TypeDescription = EventTypes.EmailSent.GetDescription(), MessageBegin = "Invalid project setting"
            };

        return alarmPayload.AlarmChannel?.Type switch
        {
            "telegram" => await SendTelegramAsync(globalSetting.TelegramOptions, project, alarmPayload),
            _ => await SendLineAsync(globalSetting.LineOptions, project, alarmPayload)
        };
    }

    private async Task<MessageEvent> SendLineAsync(LineOptions lineOptions, Project project, AlarmPayload alarmPayload)
    {
        try
        {
            logger.LogInformation("Client: {ProjectId} - {DongleId}", alarmPayload.ProjectId, alarmPayload.DongleId);
            logger.LogInformation("Server: {ProjectName} - {ProjectId} - {DongleId}", project.ProjectName,
                project.ProjectId, project.DongleId);
            logger.LogInformation("LINE Token: {Token}",
                string.IsNullOrEmpty(alarmPayload.Token)
                    ? $"Server - {project.ProjectOptions.LineOptions.Token}"
                    : $"Client - {alarmPayload.Token}");
            if (!(project.ProjectId == alarmPayload.ProjectId &&
                  project.DongleId.Equals(alarmPayload.DongleId, StringComparison.OrdinalIgnoreCase)))
                return new MessageEvent
                {
                    IsSuccess = false, Type = (int)EventTypes.Error,
                    TypeDescription = EventTypes.InvalidLicense.GetDescription(), MessageBegin = "Invalid license"
                };
            if (alarmPayload.AlarmChannel is not LineChannel lineChannel)
                return new MessageEvent
                {
                    IsSuccess = false, Type = (int)EventTypes.LineError,
                    TypeDescription = EventTypes.LineError.GetDescription(), MessageBegin = "Invalid LINE channel"
                };
            if (lineChannel.Message is null)
                return new MessageEvent
                {
                    IsSuccess = false, Type = (int)EventTypes.LineError,
                    TypeDescription = EventTypes.LineError.GetDescription(), MessageBegin = "LINE message missing"
                };
            var message = lineChannel.Message;
            switch (message?.Type)
            {
                default:
                    if (message is not FlexMessage flexMessage)
                        return new MessageEvent
                        {
                            IsSuccess = false, Type = (int)EventTypes.LineError,
                            TypeDescription = EventTypes.LineError.GetDescription(),
                            MessageBegin = "Invalid message format"
                        };

                    flexMessage.AltText = flexMessage.AltText.Length > 100
                        ? flexMessage.AltText[..100]
                        : flexMessage.AltText;

                    var pushMessagePayload = new PushMessagePayload
                    {
                        To = alarmPayload.ChatId,
                        Messages = [flexMessage]
                    };

                    logger.LogInformation("Chat ID: {ChatId}", alarmPayload.ChatId);
                    logger.LogInformation("Payload: {Payload}",
                        JsonSerializer.Serialize(pushMessagePayload, JsonHelper.IgnoreNullJsonSerializerOptions));

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        string.IsNullOrEmpty(alarmPayload.Token)
                            ? project.ProjectOptions.LineOptions.Token
                            : alarmPayload.Token);
                    httpClient.DefaultRequestHeaders.Add("X-Line-Retry-Key", Guid.NewGuid().ToString());

                    var response = await httpClient.PostAsJsonAsync(lineOptions.Url, pushMessagePayload,
                        JsonHelper.IgnoreNullJsonSerializerOptions);

                    response.EnsureSuccessStatusCode();
                    logger.LogInformation("LINE sent: {GroupId}", alarmPayload.ChatId);

                    return new MessageEvent
                    {
                        IsSuccess = true, Type = (int)EventTypes.LineSent,
                        TypeDescription = EventTypes.LineSent.GetDescription(), MessageBegin = "LINE sent"
                    };
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error occurred while sending LINE: {Message}", ex.Message);
            return new MessageEvent
            {
                IsSuccess = false, Type = (int)EventTypes.LineError,
                TypeDescription = EventTypes.LineError.GetDescription(),
                MessageBegin = ex.Message
            };
        }
    }

    private async Task<MessageEvent> SendTelegramAsync(TelegramOptions telegramOptions, Project project,
        AlarmPayload alarmPayload)
    {
        try
        {
            if (alarmPayload.AlarmChannel is not TelegramChannel telegramChannel)
                return new MessageEvent
                {
                    IsSuccess = false, Type = (int)EventTypes.TelegramError,
                    TypeDescription = EventTypes.TelegramError.GetDescription(),
                    MessageBegin = "Invalid Telegram channel"
                };
            if (telegramChannel.Message is null)
                return new MessageEvent
                {
                    IsSuccess = false, Type = (int)EventTypes.TelegramError,
                    TypeDescription = EventTypes.TelegramError.GetDescription(),
                    MessageBegin = "Telegram message missing"
                };
            var message = telegramChannel.Message;
            switch (message?.Type)
            {
                default:
                    if (message is not TextMessage textMessage)
                        return new MessageEvent
                        {
                            IsSuccess = false, Type = (int)EventTypes.TelegramError,
                            TypeDescription = EventTypes.TelegramError.GetDescription(),
                            MessageBegin = "Invalid message format"
                        };

                    var getUri =
                        $"{telegramOptions.Url}/bot{(string.IsNullOrEmpty(alarmPayload.Token) ? project.ProjectOptions.TelegramOptions.Token : alarmPayload.Token)}/sendMessage?chat_id={alarmPayload.ChatId}&parse_mode=HTML&text={textMessage.Text}";
                    var response = await httpClient.GetAsync(getUri);
                    response.EnsureSuccessStatusCode();
                    logger.LogInformation("Telegram sent: {GroupId}", alarmPayload.ChatId);

                    return new MessageEvent
                    {
                        IsSuccess = true, Type = (int)EventTypes.TelegramSent,
                        TypeDescription = EventTypes.TelegramSent.GetDescription(), MessageBegin = "Telegram sent"
                    };
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error occurred while sending Telegram: {Message}", ex.Message);
            return new MessageEvent
            {
                IsSuccess = false, Type = (int)EventTypes.TelegramError,
                TypeDescription = EventTypes.TelegramError.GetDescription(),
                MessageBegin = ex.Message
            };
        }
    }
}