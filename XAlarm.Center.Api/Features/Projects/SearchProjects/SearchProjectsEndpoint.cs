using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Projects.SearchProjects;

public class SearchProjectsEndpoint(ApplicationDbContext dbContext)
    : Endpoint<SearchProjectsRequest, SearchProjectsResponse>
{
    public override void Configure()
    {
        Post("api/projects/search");
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("Projects"));
    }

    public override async Task HandleAsync(SearchProjectsRequest request, CancellationToken cancellationToken)
    {
        var projects = await dbContext.Projects.AsNoTracking().OrderBy(x => x.ProjectName)
            .ToListAsync(cancellationToken);
        await Send.ResultAsync(TypedResults.Ok(new SearchProjectsResponse(projects)));
    }
}