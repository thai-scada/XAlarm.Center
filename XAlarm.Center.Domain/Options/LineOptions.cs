namespace XAlarm.Center.Domain.Options;

public sealed class LineOptions : AlarmOptions
{
    public string Url { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public string GetTargetLimitThisMonthUrl { get; init; } = string.Empty;
    public string GetNumberOfMessagesSentThisMonthUrl { get; init; } = string.Empty;
    public string GetNumberOfUsersInGroupChatUrl { get; init; } = string.Empty;
    public int TargetLimitThisMonth { get; init; }
    public int NumberOfMessagesSentThisMonth { get; set; }
    public DateTime UpdatedAtUtc { get; init; } = DateTime.UtcNow;
}

public sealed record TargetLimitThisMonth(string Type, int Value);

public sealed record NumberOfMessagesSentThisMonth(int TotalUsage);

public sealed record NumberOfUsersInGroupChat(int Count);