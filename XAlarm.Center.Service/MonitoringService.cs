using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Shared;
using XAlarm.Center.Service.Abstractions;

namespace XAlarm.Center.Service;

internal sealed class MonitoringService(HttpClient httpClient) : IMonitoringService
{
    public async Task<Result<string>> GetServiceStatusAsync(ServiceInfo serviceInfo)
    {
        var response = await httpClient.GetAsync(serviceInfo.DomainName);
        return !response.IsSuccessStatusCode
            ? Result.Failure<string>(new Error("Error", $"{(int)response.StatusCode} Bad Gateway"))
            : Result.Success<string>("Success");
    }
}