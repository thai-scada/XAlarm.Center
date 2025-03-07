namespace XAlarm.Center.Domain.Options;

public sealed class TelegramOptions : AlarmOptions
{
    public string Url { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public string ChatId { get; init; } = string.Empty;
}