using XAlarm.Center.Domain.Messages;

namespace XAlarm.Center.Api.Features.Alarms.SendMessage;

public record SendMessageRequest(AlarmPayload AlarmPayload);