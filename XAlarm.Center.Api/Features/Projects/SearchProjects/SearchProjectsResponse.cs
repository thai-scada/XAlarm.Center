using XAlarm.Center.Contract.Projects;

namespace XAlarm.Center.Api.Features.Projects.SearchProjects;

public record SearchProjectsResponse(IReadOnlyList<ProjectDto> Projects);