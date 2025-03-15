using XAlarm.Center.Domain.Events;

namespace XAlarm.Center.Api.Features.Alarms.SendMessage;

public record SendMessageResponse(MessageEvent MessageEvent);