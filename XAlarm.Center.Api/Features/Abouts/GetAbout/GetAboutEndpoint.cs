using FastEndpoints;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Abouts.GetAbout;

public class GetAboutEndpoint : EndpointWithoutRequest<GetAboutResponse>
{
    public override void Configure()
    {
        Get("api/abouts/get");
        Policies(RoleTypes.RealmBasic.GetDescription());
        Description(x => x.WithTags("Abouts"));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await Send.ResultAsync(TypedResults.Ok());
    }
}