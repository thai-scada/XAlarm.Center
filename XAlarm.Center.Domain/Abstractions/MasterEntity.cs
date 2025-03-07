namespace XAlarm.Center.Domain.Abstractions;

public abstract class MasterEntity : Entity
{
    public int MasterId { get; init; }
    public string Name { get; protected init; } = string.Empty;
    public string NameAlt { get; set; } = string.Empty;
    public string NameAbbr { get; init; } = string.Empty;
    public string Description { get; protected init; } = string.Empty;
    public bool Enable { get; protected init; }
    public bool Visible { get; init; }
    public int SortOrder { get; protected init; }
    public int Code { get; init; }
    public string Key { get; init; } = string.Empty;
    public int Type { get; protected init; }
    public int Status { get; init; }
    public string Tags { get; set; } = string.Empty;
    public string Hashtags { get; init; } = string.Empty;
    public bool IsDefault { get; init; }
    public DateTime? CreatedOnUtc { get; protected init; }
    public Guid? CreatedUserId { get; init; }
    public DateTime? UpdatedOnUtc { get; init; }
    public Guid? UpdatedUserId { get; init; }
}