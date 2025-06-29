using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Domain.Options;

public sealed class AppOptions : BaseAppOptions
{
    public RabbitMqOptions RabbitMqOptions { get; init; } = new();
    public ResetMessageQuotaJobOptions ResetMessageQuotaJobOptions { get; init; } = new();
    public ServiceMonitoringJobOptions ServiceMonitoringJobOptions { get; init; } = new();
}