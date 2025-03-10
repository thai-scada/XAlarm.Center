using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Domain.Projects;

public sealed class Project : Entity
{
    public Guid ProjectId { get; init; }
    public Guid ProjectGroupId { get; init; }
    public string ProjectName { get; init; } = string.Empty;
    public DateTime ValidUntil { get; init; }
    public string InvoiceNo { get; init; } = string.Empty;
    public string DongleId { get; init; } = string.Empty;
    public ProjectOptions ProjectOptions { get; init; } = new();
}