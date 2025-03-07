namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class PushMessagePayload
{
    public string To { get; set; } = string.Empty;
    public LineMessage[] Messages { get; set; } = [];
}