using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Projects.SearchProjects;

public static class SearchProjectsErrors
{
    public static readonly Error Error = new("SearchProjects.Error", "An error occurred while searching projects");
}