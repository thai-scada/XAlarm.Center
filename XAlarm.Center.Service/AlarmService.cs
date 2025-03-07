using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Service.Abstractions;

namespace XAlarm.Center.Service;

internal sealed class AlarmService : IAlarmService
{
    public Task SendAsync(AlarmPayload alarmPayload)
    {
        throw new NotImplementedException();
    }
}