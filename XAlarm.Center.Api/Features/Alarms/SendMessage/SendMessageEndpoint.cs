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
        Description(x => x.WithTags("Alarms"));
    }

    public override async Task HandleAsync(SendMessageRequest request, CancellationToken cancellationToken)
    {
        await Send.ResultAsync(
            TypedResults.Ok(new SendMessageResponse(await alarmService.SendMessageAsync(request.AlarmPayload))));
    }
}