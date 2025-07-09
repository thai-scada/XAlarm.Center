using XAlarm.Center.Contract.Abstractions;
using XAlarm.Center.Domain.Projects;

namespace XAlarm.Center.Contract.Projects;

public sealed record ProjectDto : Dto
{
    public Guid ProjectId { get; init; }
    public Guid ProjectGroupId { get; init; }
    public string ProjectName { get; init; } = string.Empty;
    public DateTime ValidUntil { get; init; }
    public string InvoiceNo { get; init; } = string.Empty;
    public string DongleId { get; init; } = string.Empty;
    public ProjectOptions ProjectOptions { get; init; } = new();
}