using XAlarm.Center.Domain.Messages;

namespace XAlarm.Center.Service.Abstractions;

public interface IAlarmService
{
    Task SendAsync(AlarmPayload alarmPayload);
}