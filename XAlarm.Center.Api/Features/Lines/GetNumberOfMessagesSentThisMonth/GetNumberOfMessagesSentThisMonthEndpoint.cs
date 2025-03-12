using FastEndpoints;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Lines.GetNumberOfMessagesSentThisMonth;

public class GetNumberOfMessagesSentThisMonthEndpoint(ILineService lineService)
    : Endpoint<GetNumberOfMessagesSentThisMonthRequest, GetNumberOfMessagesSentThisMonthResponse>
{
    public override void Configure()
    {
        Get("api/lines/getNumberOfMessagesSentThisMonth/{projectId}");
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("LINE"));
    }

    public override async Task HandleAsync(GetNumberOfMessagesSentThisMonthRequest request,
        CancellationToken cancellationToken)
    {
        await SendResultAsync(TypedResults.Ok(
            new GetNumberOfMessagesSentThisMonthResponse(
                await lineService.GetNumberOfMessagesSentThisMonthAsync(request.ProjectId))));
    }
}