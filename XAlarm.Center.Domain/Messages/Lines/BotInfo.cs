namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class BotInfo
{
    public string UserId { get; set; } = string.Empty;
    public string BasicId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string ChatMode { get; set; } = string.Empty;
    public string MarkAsReadMode { get; set; } = string.Empty;
}