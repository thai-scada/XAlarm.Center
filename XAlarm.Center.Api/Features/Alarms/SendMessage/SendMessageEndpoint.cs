using FastEndpoints;
using XAlarm.Center.Service.Abstractions;

namespace XAlarm.Center.Api.Features.Alarms.SendMessage;

public class SendMessageEndpoint(IAlarmService alarmService) : Endpoint<SendMessageRequest, SendMessageResponse>
{
    public override void Configure()
    {
        Get("api/alarms/sendMessage");
        // Policies(RoleTypes.RealmBasic.GetDescription());
        AllowAnonymous();
        Description(x => x.WithTags("Abouts"));
    }

    public override async Task HandleAsync(SendMessageRequest request, CancellationToken cancellationToken)
    {
        await SendResultAsync(
            TypedResults.Ok(new SendMessageResponse(await alarmService.SendAsync(request.AlarmPayload))));
    }
}