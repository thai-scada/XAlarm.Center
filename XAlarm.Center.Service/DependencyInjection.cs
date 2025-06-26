using Microsoft.Extensions.DependencyInjection;
using Quartz;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Service.Jobs;

namespace XAlarm.Center.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddHttpClient<IAlarmService, AlarmService>();
        services.AddHttpClient<ILineService, LineService>();

        services.AddTransient<ResetMessageQuotaJob>();
        services.AddQuartz(quartz =>
        {
            quartz.ScheduleJob<ResetMessageQuotaJob>(trigger => trigger
                .WithIdentity($"reset-message-quota-job")
                .WithCronSchedule("0 0 0 1 * ? *")
                .WithDescription("Reset message quota")
            );
        });
        services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });

        return services;
    }
}