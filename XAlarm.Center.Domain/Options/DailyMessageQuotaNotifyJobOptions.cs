namespace XAlarm.Center.Domain.Options;

public sealed class DailyMessageQuotaNotifyJobOptions : JobOptions
{
    public Guid ProjectId { get; init; }
    public string Token { get; init; } = string.Empty;
    public string ChatId { get; init; } = string.Empty;
}