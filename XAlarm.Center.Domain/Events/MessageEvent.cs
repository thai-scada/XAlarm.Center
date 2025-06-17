using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Messages;

namespace XAlarm.Center.Domain.Events;

public sealed class MessageEvent : Entity
{
    public Guid ProjectId { get; init; }
    public AlarmPayload AlarmPayload { get; init; } = new();
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public DateTime EventBeginOnUtc { get; init; }
    public DateTime? EventEndOnUtc { get; init; }
    public int Type { get; init; }
    public string TypeDescription { get; init; } = string.Empty;
    public string MessageBegin { get; init; } = string.Empty;
    public string MessageEnd { get; init; } = string.Empty;
    public string CreatedBy { get; init; } = string.Empty;
}