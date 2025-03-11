using FastEndpoints;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Alarms.SendMessage;

public class SendMessageEndpoint(IAlarmService alarmService) : Endpoint<SendMessageRequest, SendMessageResponse>
{
    public override void Configure()
    {
        Post("api/alarms/sendMessage");
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("Abouts"));
    }

    public override async Task HandleAsync(SendMessageRequest request, CancellationToken cancellationToken)
    {
        await SendResultAsync(
            TypedResults.Ok(new SendMessageResponse(await alarmService.SendAsync(request.AlarmPayload))));
    }
}