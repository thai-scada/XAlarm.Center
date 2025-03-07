using Microsoft.Extensions.Options;
using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Api.Options;

public class AppOptionsSetup(IConfiguration configuration) : IConfigureOptions<AppOptions>
{
    public void Configure(AppOptions options)
    {
        configuration.GetSection(nameof(AppOptions)).Bind(options);
    }
}