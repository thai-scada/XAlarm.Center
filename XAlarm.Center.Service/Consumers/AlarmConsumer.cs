using MassTransit;
using Microsoft.Extensions.Logging;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Service.Abstractions;

namespace XAlarm.Center.Service.Consumers;

public class AlarmConsumer(ILogger<AlarmConsumer> logger, IAlarmService alarmService) : IConsumer<AlarmPayload>
{
    public async Task Consume(ConsumeContext<AlarmPayload> context)
    {
        var routingKey = context.GetHeader("RabbitMQ-RoutingKey");
        logger.LogInformation("{Consumer}: {RoutingKey}", nameof(AlarmConsumer), routingKey);

        await alarmService.SendAsync(context.Message);
    }
}