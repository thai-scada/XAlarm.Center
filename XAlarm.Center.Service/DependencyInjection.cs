using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Service.Consumers;

namespace XAlarm.Center.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddMassTransit(massTransit =>
        {
            massTransit.AddConsumer<AlarmConsumer>();
            massTransit.UsingRabbitMq((context, configure) =>
            {
                configure.Host(appOptions.RabbitMqOptions.Host, appOptions.RabbitMqOptions.VirtualHost,
                    host =>
                    {
                        host.Username(appOptions.RabbitMqOptions.Username);
                        host.Password(appOptions.RabbitMqOptions.Password);
                    });
                configure.ReceiveEndpoint("x-alarm", endpoint =>
                {
                    endpoint.ConfigureConsumer<AlarmConsumer>(context);
                    endpoint.UseRawJsonDeserializer(isDefault: true);
                });
                configure.ConfigureEndpoints(context);
            });
        });

        services.AddHttpClient<IAlarmService, AlarmService>();

        return services;
    }
}