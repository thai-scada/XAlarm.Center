using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XAlarm.Center.Domain.Events;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Messages.Lines;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Projects;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;
using XAlarm.Center.Shared.Helpers;

// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataQuery

namespace XAlarm.Center.Service;

internal sealed class AlarmService(ILogger<AlarmService> logger, ApplicationDbContext dbContext, HttpClient httpClient)
    : IAlarmService
{
    public async Task<Event> SendAsync(AlarmPayload alarmPayload)
    {
        var globalSetting = await dbContext.GlobalSettings.AsNoTracking().SingleOrDefaultAsync();

        if (globalSetting is null)
            return new Event
            {
                Type = (int)EventTypes.Error, TypeDescription = EventTypes.EmailSent.GetDescription(),
                MessageBegin = "Invalid system setting"
            };

        var project = await dbContext.Projects.AsNoTracking()
            .SingleOrDefaultAsync(x => x.ProjectId == alarmPayload.ProjectId);

        if (project is null)
            return new Event
            {
                Type = (int)EventTypes.Error, TypeDescription = EventTypes.EmailSent.GetDescription(),
                MessageBegin = "Invalid project setting"
            };

        return alarmPayload.ChannelId switch
        {
            _ => await SendLineAsync(globalSetting.LineOptions, project, alarmPayload)
        };
    }

    private async Task<Event> SendLineAsync(LineOptions lineOptions, Project project, AlarmPayload alarmPayload)
    {
        try
        {
            if (alarmPayload.Message is null)
                return new Event
                {
                    Type = (int)EventTypes.Error, TypeDescription = EventTypes.EmailSent.GetDescription(),
                    MessageBegin = "Message missing"
                };
            var message = alarmPayload.Message as LineMessage;
            switch (message?.Type)
            {
                default:
                    if (message is not FlexMessage flexMessage)
                        return new Event
                        {
                            Type = (int)EventTypes.Error, TypeDescription = EventTypes.EmailSent.GetDescription(),
                            MessageBegin = "Invalid message format"
                        };
                    var pushMessagePayload = new PushMessagePayload
                    {
                        To = alarmPayload.ChatId ?? string.Empty,
                        Messages = [flexMessage]
                    };

                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer",
                            alarmPayload.Token ?? project.ProjectOptions.LineOptions.Token);
                    httpClient.DefaultRequestHeaders.Add("X-Line-Retry-Key", Guid.NewGuid().ToString());

                    // var response = await httpClient.PostAsJsonAsync(lineOptions.Url, pushMessagePayload,
                    //     JsonHelper.IgnoreNullJsonSerializerOptions);
                    //
                    // response.EnsureSuccessStatusCode();
                    logger.LogInformation("LINE sent: {GroupId}", alarmPayload.ChatId);

                    return new Event
                    {
                        Type = (int)EventTypes.LineSent, TypeDescription = EventTypes.LineSent.GetDescription(),
                        MessageBegin = "LINE sent"
                    };
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error occurred while sending LINE: {Message}", ex.Message);
            return new Event
            {
                Type = (int)EventTypes.LineError, TypeDescription = EventTypes.LineError.GetDescription(),
                MessageBegin = $"Error occurred while sending 'LINE': {ex.Message}"
            };
        }
    }
}