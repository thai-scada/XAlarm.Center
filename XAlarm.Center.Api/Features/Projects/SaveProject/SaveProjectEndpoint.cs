using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Projects.SaveProject;

public class SaveProjectEndpoint(ApplicationDbContext dbContext) : Endpoint<SaveProjectRequest, SaveProjectResponse>
{
    public override void Configure()
    {
        Post("api/projects/save");
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("Projects"));
    }

    public override async Task HandleAsync(SaveProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Project.Id, cancellationToken: cancellationToken);
        if (project is not null)
            await dbContext.Projects.AsNoTracking().Where(x => x.Id == project.Id).ExecuteUpdateAsync(
                x => x.SetProperty(y => y.ProjectId, request.Project.ProjectId)
                    .SetProperty(y => y.ProjectGroupId, request.Project.ProjectGroupId)
                    .SetProperty(y => y.ProjectName, request.Project.ProjectName)
                    .SetProperty(y => y.ValidUntil, request.Project.ValidUntil)
                    .SetProperty(y => y.InvoiceNo, request.Project.InvoiceNo)
                    .SetProperty(y => y.DongleId, request.Project.DongleId)
                    .SetProperty(y => y.ProjectOptions, request.Project.ProjectOptions), cancellationToken);
        else
        {
            await dbContext.Projects.AddAsync(request.Project, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        await SendResultAsync(TypedResults.Ok(new SaveProjectResponse(request.Project.Id)));
    }
}