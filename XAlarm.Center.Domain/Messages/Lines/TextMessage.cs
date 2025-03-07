namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class TextMessage : LineMessage
{
    public string Text { get; init; } = string.Empty;
}