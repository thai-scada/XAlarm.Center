namespace XAlarm.Center.Domain.Options;

public sealed class ResetMessageQuotaJobOptions
{
    public string CronExpression { get; set; } = string.Empty;
}