using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Messages.Lines;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Shared;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;
using XAlarm.Center.Shared.Helpers;

namespace XAlarm.Center.Service.Jobs;

public class ServiceMonitoringJob(
    ILogger<ServiceMonitoringJob> logger,
    IOptions<AppOptions> appOptions,
    IMonitoringService monitoringService,
    IAlarmService alarmService,
    ILineService lineService) : IJob
{
    private readonly AppOptions _appOptions = appOptions.Value;

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Job '{JobName}' started at: {Now}", context.JobDetail.Key.Name, DateTime.Now);

        if (_appOptions.ServiceMonitoringJobOptions.ServiceInfos is not [])
        {
            foreach (var serviceInfo in _appOptions.ServiceMonitoringJobOptions.ServiceInfos.Where(x => x.EnableNotify))
            {
                var response = await monitoringService.GetServiceStatusAsync(serviceInfo);
                if (response.IsSuccessStatusCode) continue;
                const string message =
                    """{"altText":"Service Unavailable","contents":{"type":"bubble","body":{"type":"box","layout":"vertical","contents":[{"type":"text","text":"{{title}}","color":"#ff0000","weight":"bold","size":"lg"},{"type":"box","layout":"vertical","margin":"lg","spacing":"sm","contents":[{"type":"box","layout":"baseline","spacing":"sm","contents":[{"type":"text","flex":1,"text":"Desc.","color":"#757575","size":"sm"},{"type":"text","flex":5,"text":"{{message}}","wrap":true,"color":"#666666","size":"sm"}]},{"type":"box","layout":"baseline","spacing":"sm","contents":[{"type":"text","flex":1,"text":"Time","color":"#757575","size":"sm"},{"type":"text","flex":5,"text":"{{timestamp}}","wrap":true,"color":"#666666","size":"sm"}]}]},{"type":"text","text":"{{quota}}","color":"#0000ff","size":"xxs","align":"end","offsetTop": "5px"}]}},"type":"flex"}""";
                var quota = await lineService.GetQuotaMessageThisMonthAsync(serviceInfo.ProjectId, serviceInfo.ChatId,
                    string.Empty, 1);
                var alarmPayload = new AlarmPayload
                {
                    ProjectId = serviceInfo.ProjectId,
                    ChatId = serviceInfo.ChatId,
                    AlarmChannel = new LineChannel
                    {
                        Type = AlarmChannels.Line.GetDescription(),
                        Message = JsonSerializer.Deserialize<LineMessage>(
                            DecodeMessage(message, serviceInfo, response, quota),
                            JsonHelper.DefaultJsonSerializerOptions)
                    }
                };
                await alarmService.SendMessageAsync(alarmPayload);
            }
        }
    }

    private string DecodeMessage(string message, ServiceInfo serviceInfo, HttpResponseMessage response, string quota)
    {
        var variables = message.BetweenRange("{{", "}}");

        return variables.Aggregate(message, (current, variable) => variable switch
        {
            "title" => current.Replace("{{" + variable + "}}", serviceInfo.Title),
            "message" => current.Replace("{{" + variable + "}}", $"{(int)response.StatusCode} {response.ReasonPhrase}"),
            "quota" => current.Replace("{{" + variable + "}}", quota),
            "timestamp" => current.Replace("{{" + variable + "}}",
                DateTimeHelper.DateTimeToString(DateTimeHelper.ConvertByTimeZone(DateTime.UtcNow, _appOptions.TimeZone),
                    "ddd, MMM dd, yyyy, HH:mm")),
            _ => current
        });
    }
}