namespace XAlarm.Center.Domain.Options;

public sealed class LineOptions : AlarmOptions
{
    public string Url { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public string GetTargetLimitThisMonthUrl { get; init; } = string.Empty;
    public string GetNumberOfMessagesSentThisMonthUrl { get; init; } = string.Empty;
}

public sealed record TargetLimitThisMonth(string Type, int Value);

public sealed record NumberOfMessagesSentThisMonth(int TotalUsage);