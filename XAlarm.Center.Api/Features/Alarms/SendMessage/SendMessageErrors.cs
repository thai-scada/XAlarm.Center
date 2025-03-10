using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Alarms.SendMessage;

public static class SendMessageErrors
{
    public static readonly Error Error = new("SendMessage.Error", "An error occurred while sending message");
}