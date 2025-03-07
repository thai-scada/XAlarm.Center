namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class ImageMessage : LineMessage
{
    public string OriginalContentUrl { get; set; } = string.Empty;
    public string PreviewImageUrl { get; set; } = string.Empty;
}