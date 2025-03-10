using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Domain.Events;

public sealed class Event : Entity
{
    public DateTime EventBeginOnUtc { get; init; }
    public DateTime? EventEndOnUtc { get; init; }
    public int Type { get; init; }
    public string TypeDescription { get; init; } = string.Empty;
    public string MessageBegin { get; init; } = string.Empty;
    public string MessageEnd { get; init; } = string.Empty;
    public string CreatedBy { get; init; } = string.Empty;
}