using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Messages.Lines;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;
using XAlarm.Center.Shared.Helpers;

// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataQuery

namespace XAlarm.Center.Service.Jobs;

public class DailyMessageQuotaNotifyJob(
    ILogger<DailyMessageQuotaNotifyJob> logger,
    IOptions<AppOptions> appOptions,
    ApplicationDbContext dbContext,
    IAlarmService alarmService,
    ILineService lineService)
    : IJob
{
    private readonly AppOptions _appOptions = appOptions.Value;

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Job '{JobName}' started at: {Now}", context.JobDetail.Key.Name, DateTime.Now);

        var projects = await dbContext.Projects.AsNoTracking().ToListAsync();
        var projectsByTokens = projects
            .Where(x => x.ProjectOptions.LineOptions.TokenProvider == (int)TokenProviders.CommercialServer)
            .GroupBy(x => x.ProjectOptions.LineOptions.Token).ToList();
        if (projectsByTokens is not [])
        {
            foreach (var projectsByToken in projectsByTokens)
            {
                var botInfo = await lineService.GetBotInfoAsync(projectsByToken.Key);
                var message =
                    """{"altText":"Message Quota","contents":{"type":"bubble","body":{"type":"box","layout":"vertical","contents":[{"type":"text","text":"{{botName}}","color":"#ff0000","weight":"bold","size":"lg"},{"type":"box","layout":"vertical","margin":"lg","spacing":"sm","contents":[{"type":"box","layout":"baseline","spacing":"sm","contents":[{"type":"text","flex":1,"text":"Time","color":"#757575","size":"sm"},{"type":"text","flex":5,"text":"{{timestamp}}","wrap":true,"color":"#666666","size":"sm"}]}]},{"type":"text","text":"{{quota}}","color":"#0000ff","size":"xxs","align":"end","offsetTop":"5px"}]}},"type":"flex"}""";
                var quota = await lineService.GetQuotaMessageThisMonthAsync(
                    _appOptions.DailyMessageQuotaNotifyJobOptions.ProjectId,
                    _appOptions.DailyMessageQuotaNotifyJobOptions.ChatId,
                    string.Empty, 1);
                message = DecodeMessage(message, botInfo, quota);
                var flexMessage =
                    JsonSerializer.Deserialize<FlexMessage>(message, JsonHelper.DefaultJsonSerializerOptions);
                if (flexMessage is null) continue;
                var i = 1;
                foreach (var project in projectsByToken)
                {
                    var content = new Content
                    {
                        Type = "box",
                        Layout = "baseline",
                        Spacing = "sm",
                        Contents =
                        [
                            new Content
                            {
                                Type = "text",
                                Flex = 1,
                                Text = project.ProjectName,
                                Color = "#757575",
                                Size = "sm"
                            },
                            new Content
                            {
                                Type = "text",
                                Flex = 5,
                                Text = await lineService.GetQuotaMessageThisMonthAsync(project.ProjectId, string.Empty,
                                    string.Empty),
                                Wrap = true,
                                Color = "#666666",
                                Size = "sm"
                            }
                        ]
                    };
                    flexMessage.Contents?.Body?.Contents?.ToList().Insert(i, content);
                    i++;
                }

                var alarmPayload = new AlarmPayload
                {
                    ProjectId = _appOptions.DailyMessageQuotaNotifyJobOptions.ProjectId,
                    ChatId = _appOptions.DailyMessageQuotaNotifyJobOptions.ChatId,
                    AlarmChannel = new LineChannel
                    {
                        Type = AlarmChannels.Line.GetDescription(),
                        Message = flexMessage
                    }
                };
                await alarmService.SendMessageAsync(alarmPayload);
            }
        }
    }

    private string DecodeMessage(string message, BotInfo botInfo, string quota)
    {
        var variables = message.BetweenRange("{{", "}}");

        return variables.Aggregate(message, (current, variable) => variable switch
        {
            "botName" => current.Replace("{{" + variable + "}}", botInfo.DisplayName),
            "quota" => current.Replace("{{" + variable + "}}", quota),
            "timestamp" => current.Replace("{{" + variable + "}}",
                DateTimeHelper.DateTimeToString(DateTimeHelper.ConvertByTimeZone(DateTime.UtcNow, _appOptions.TimeZone),
                    "ddd, MMM dd, yyyy, HH:mm")),
            _ => current
        });
    }
}