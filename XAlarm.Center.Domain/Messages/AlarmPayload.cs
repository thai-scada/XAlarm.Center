using MassTransit;

namespace XAlarm.Center.Domain.Messages;

[MessageUrn("alarm-payload")]
public sealed class AlarmPayload
{
    public string Topic { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}