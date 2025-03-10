namespace XAlarm.Center.Domain.Options;

public sealed class LineOptions : AlarmOptions
{
    public string Url { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}