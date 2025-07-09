using XAlarm.Center.Domain.Projects;

namespace XAlarm.Center.Api.Features.Projects.SearchProjects;

public record SearchProjectsResponse(IReadOnlyList<Project> Projects);