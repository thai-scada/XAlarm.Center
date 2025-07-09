namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class BotInfo
{
    public string UserId { get; init; } = string.Empty;
    public string BasicId { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string ChatMode { get; init; } = string.Empty;
    public string MarkAsReadMode { get; init; } = string.Empty;
    public string Quota { get; set; } = string.Empty;
}