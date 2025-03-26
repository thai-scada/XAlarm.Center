using FastEndpoints;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Lines.GetTargetLimitThisMonth;

public class GetTargetLimitThisMonthEndpoint(ILineService lineService)
    : Endpoint<GetTargetLimitThisMonthRequest, GetTargetLimitThisMonthResponse>
{
    public override void Configure()
    {
        Post("api/lines/getTargetLimitThisMonth");
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("LINE"));
    }

    public override async Task HandleAsync(GetTargetLimitThisMonthRequest request, CancellationToken cancellationToken)
    {
        await SendResultAsync(TypedResults.Ok(
            new GetTargetLimitThisMonthResponse(
                await lineService.GetTargetLimitThisMonthAsync(request.ProjectId, request.Token))));
    }
}