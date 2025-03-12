namespace XAlarm.Center.Domain.Messages;

public sealed class AlarmPayload
{
    public Guid ProjectId { get; init; }
    public string DongleId { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public string ChatId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public AlarmChannel? AlarmChannel { get; init; }
}