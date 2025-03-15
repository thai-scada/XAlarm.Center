using XAlarm.Center.Domain.Events;
using XAlarm.Center.Domain.Messages;

namespace XAlarm.Center.Service.Abstractions;

public interface IAlarmService
{
    Task<MessageEvent> SendMessageAsync(AlarmPayload alarmPayload);
}