using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Abouts.GetAbout;

public static class GetAboutErrors
{
    public static readonly Error Error = new("GetAbout.Error", "An error occurred while getting about information");
}