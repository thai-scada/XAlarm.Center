namespace XAlarm.Center.Domain.Shared;

public sealed class ServiceInfo
{
    public string Name { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string DomainName { get; init; } = string.Empty;
    public bool EnableNotify { get; init; }
    public Guid ProjectId { get; set; }
    public string Token { get; init; } = string.Empty;
    public string ChatId { get; init; } = string.Empty;
}