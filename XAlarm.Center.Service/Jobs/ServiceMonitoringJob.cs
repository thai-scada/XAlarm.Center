using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Service.Abstractions;

namespace XAlarm.Center.Service.Jobs;

public class ServiceMonitoringJob(
    ILogger<ServiceMonitoringJob> logger,
    IOptions<AppOptions> appOptions,
    IMonitoringService monitoringService) : IJob
{
    private readonly AppOptions _appOptions = appOptions.Value;

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Job '{JobName}' started at: {Now}", context.JobDetail.Key.Name, DateTime.Now);

        if (_appOptions.ServiceMonitoringJobOptions.ServiceInfos is not [])
        {
            foreach (var serviceInfo in _appOptions.ServiceMonitoringJobOptions.ServiceInfos)
            {
                var result = await monitoringService.GetServiceStatusAsync(serviceInfo);
            }
        }
    }
}