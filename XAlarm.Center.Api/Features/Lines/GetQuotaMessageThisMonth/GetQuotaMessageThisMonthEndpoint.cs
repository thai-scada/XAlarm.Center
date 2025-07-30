using FastEndpoints;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Service.Abstractions;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Lines.GetQuotaMessageThisMonth;

public class GetQuotaMessageThisMonthEndpoint(ILineService lineService)
    : Endpoint<GetQuotaMessageThisMonthRequest, GetQuotaMessageThisMonthResponse>
{
    public override void Configure()
    {
        Post("api/lines/getQuotaMessageThisMonth");
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("LINE"));
    }

    public override async Task HandleAsync(GetQuotaMessageThisMonthRequest request,
        CancellationToken cancellationToken)
    {
        await Send.ResultAsync(TypedResults.Ok(new GetQuotaMessageThisMonthResponse(
            await lineService.GetQuotaMessageThisMonthAsync(request.ProjectId, request.ChatId, request.Token,
                request.Mode))));
    }
}