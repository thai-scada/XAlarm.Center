namespace XAlarm.Center.Domain.Abstractions;

public abstract class DataMapEntity : Entity
{
    public string Name { get; protected init; } = string.Empty;
    public string Description { get; protected init; } = string.Empty;
    public bool Enable { get; protected init; }
    public int SortOrder { get; protected init; }
}