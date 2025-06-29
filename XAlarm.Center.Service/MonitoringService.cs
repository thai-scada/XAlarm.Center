using XAlarm.Center.Domain.Shared;
using XAlarm.Center.Service.Abstractions;

namespace XAlarm.Center.Service;

internal sealed class MonitoringService(HttpClient httpClient) : IMonitoringService
{
    public async Task<HttpResponseMessage> GetServiceStatusAsync(ServiceInfo serviceInfo)
    {
        return await httpClient.GetAsync(serviceInfo.DomainName);
    }
}