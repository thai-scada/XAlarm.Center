using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using XAlarm.Center.Api.Extensions;
using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Projects.GetProject;

public class GetProjectEndpoint(ApplicationDbContext dbContext) : Endpoint<GetProjectRequest, GetProjectResponse>
{
    public override void Configure()
    {
        Get("api/projects/get/{id}");
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("Projects"));
    }

    public override async Task HandleAsync(GetProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (project is not null)
            await SendResultAsync(TypedResults.Ok(new GetProjectResponse(project)));
        else
            await SendResultAsync(Result.Failure(GetProjectErrors.NotFound).ToProblemDetails());
    }
}