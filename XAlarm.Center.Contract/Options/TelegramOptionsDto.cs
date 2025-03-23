namespace XAlarm.Center.Contract.Options;

public sealed record TelegramOptionsDto : AlarmOptionsDto
{
    public string Url { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}