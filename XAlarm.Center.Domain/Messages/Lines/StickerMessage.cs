namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class StickerMessage : LineMessage
{
    public string PackageId { get; init; } = string.Empty;
    public string StickerId { get; init; } = string.Empty;
}