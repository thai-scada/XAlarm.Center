using XAlarm.Center.Domain.Shared;

namespace XAlarm.Center.Domain.Options;

public sealed class ServiceMonitoringJobOptions : JobOptions
{
    public ServiceInfo[] ServiceInfos { get; set; } = [];
}