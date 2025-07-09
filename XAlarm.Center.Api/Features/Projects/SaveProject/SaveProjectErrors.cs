using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Projects.SaveProject;

public static class SaveProjectErrors
{
    public static readonly Error Error = new("SaveProject.Error", "An error occurred while saving project");
}