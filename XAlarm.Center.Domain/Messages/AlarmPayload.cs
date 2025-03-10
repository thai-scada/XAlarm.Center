namespace XAlarm.Center.Domain.Messages;

public sealed class AlarmPayload
{
    public Guid ProjectId { get; init; }
    public string DongleId { get; init; } = string.Empty;
    public int ChannelId { get; init; }
    public string Token { get; init; } = string.Empty;
    public string ChatId { get; init; } =  string.Empty;
    public string UserId { get; init; } = string.Empty;
    public AlarmMessage? Message { get; init; }
}