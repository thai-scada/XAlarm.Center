namespace XAlarm.Center.Contract.Abstractions;

public abstract record MasterDto : Dto
{
    public int MasterId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? NameAlt { get; init; }
    public string? NameAbbr { get; init; }
    public string? Description { get; set; } = string.Empty;
    public bool Enable { get; set; }
    public int SortOrder { get; set; }
    public int Code { get; set; }
    public string? Key { get; set; } = string.Empty;
    public int Type { get; set; }
    public int Status { get; set; }
    public string? Tags { get; set; }
    public bool? IsDefault { get; set; }
}