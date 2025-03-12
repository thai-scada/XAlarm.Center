using XAlarm.Center.Domain.Events;
using XAlarm.Center.Domain.Messages;

namespace XAlarm.Center.Service.Abstractions;

public interface IAlarmService
{
    Task<Event> SendMessageAsync(AlarmPayload alarmPayload);
}