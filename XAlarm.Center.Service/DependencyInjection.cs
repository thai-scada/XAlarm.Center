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
        services.AddHttpClient<IMonitoringService, MonitoringService>();

        services.AddTransient<ResetMessageQuotaJob>();
        services.AddTransient<ServiceMonitoringJob>();

        if (appOptions.ResetMessageQuotaJobOptions.Enable)
            services.AddQuartz(quartz =>
            {
                quartz.ScheduleJob<ResetMessageQuotaJob>(trigger => trigger
                    .WithIdentity("reset-message-quota-job")
                    .WithCronSchedule(appOptions.ResetMessageQuotaJobOptions.CronExpression)
                    .WithDescription("Reset message quota")
                );
            });

        if (appOptions.ServiceMonitoringJobOptions.Enable)
            services.AddQuartz(quartz =>
            {
                quartz.ScheduleJob<ServiceMonitoringJob>(trigger => trigger
                    .WithIdentity("service-monitoring-job")
                    .WithCronSchedule(appOptions.ServiceMonitoringJobOptions.CronExpression)
                    .WithDescription("Service monitoring job")
                );
            });

        if (appOptions.DailyMessageQuotaNotifyJobOptions.Enable)
            services.AddQuartz(quartz =>
            {
                quartz.ScheduleJob<DailyMessageQuotaNotifyJob>(trigger => trigger
                    .WithIdentity("daily-message-quota-notify-job")
                    .WithCronSchedule(appOptions.DailyMessageQuotaNotifyJobOptions.CronExpression)
                    .WithDescription("Daily message quota notify job")
                );
            });

        services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });

        return services;
    }
}