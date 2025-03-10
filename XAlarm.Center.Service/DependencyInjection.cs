using Microsoft.Extensions.DependencyInjection;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Service.Abstractions;

namespace XAlarm.Center.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddHttpClient<IAlarmService, AlarmService>();

        return services;
    }
}