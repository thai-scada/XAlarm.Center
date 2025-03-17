namespace XAlarm.Center.Domain.Messages.Telegrams;

public sealed class TextMessage : TelegramMessage
{
    public string Text { get; init; } = string.Empty;
}