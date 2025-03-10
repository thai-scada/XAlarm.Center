using Microsoft.Extensions.Options;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Projects;

namespace XAlarm.Center.Api.Options;

public class ProjectOptionsSetup(IConfiguration configuration) : IConfigureOptions<ProjectOptions>
{
    public void Configure(ProjectOptions options)
    {
        configuration.GetSection(nameof(ProjectOptions)).Bind(options);
    }
}