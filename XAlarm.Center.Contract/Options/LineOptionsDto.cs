namespace XAlarm.Center.Contract.Options;

public sealed record LineOptionsDto : AlarmOptionsDto
{
    public string Url { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public string GetTargetLimitThisMonthUrl { get; init; } = string.Empty;
    public string GetNumberOfMessagesSentThisMonthUrl { get; init; } = string.Empty;
}