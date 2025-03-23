using XAlarm.Center.Contract.Abstractions;

namespace XAlarm.Center.Contract.Projects;

public sealed record ProjectDto : Dto
{
    public Guid ProjectId { get; init; }
    public Guid ProjectGroupId { get; init; }
    public string ProjectName { get; init; } = string.Empty;
    public DateTime ValidUntil { get; init; }
    public string InvoiceNo { get; init; } = string.Empty;
    public string DongleId { get; init; } = string.Empty;
    public ProjectOptionsDto ProjectOptions { get; init; } = new();
}