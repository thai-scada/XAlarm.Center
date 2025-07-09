using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Projects.GetProject;

public static class GetProjectErrors
{
    public static readonly Error Error = new("GetProject.Error", "An error occurred while getting project");

    public static readonly Error NotFound = new("GetProject.NotFound",
        "The project with the specified ID was not found");
}