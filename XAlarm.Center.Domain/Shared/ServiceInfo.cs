namespace XAlarm.Center.Domain.Shared;

public sealed class ServiceInfo
{
    public string Name { get; set; } = string.Empty;
    public string DomainName { get; set; } = string.Empty;
    public int[] StatusCodes { get; set; } = [];
}