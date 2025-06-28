using XAlarm.Center.Domain.Shared;

namespace XAlarm.Center.Domain.Options;

public sealed class ServiceMonitoringJobOptions : JobOptions
{
    public LineOptions LineOptions { get; set; } = new();
    public ServiceInfo[] ServiceInfos { get; set; } = [];
}