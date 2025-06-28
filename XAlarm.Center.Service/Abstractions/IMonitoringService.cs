using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Shared;

namespace XAlarm.Center.Service.Abstractions;

public interface IMonitoringService
{
    Task<Result<string>> GetServiceStatusAsync(ServiceInfo serviceInfo);
}