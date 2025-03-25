namespace XAlarm.Center.Contract.Abstractions;

public abstract record DataMapDto : Dto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Enable { get; set; }
    public int SortOrder { get; set; }
}