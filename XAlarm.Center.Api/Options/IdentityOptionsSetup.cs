using Microsoft.Extensions.Options;
using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Api.Options;

public class IdentityOptionsSetup(IConfiguration configuration) : IConfigureOptions<IdentityOptions>
{
    public void Configure(IdentityOptions options)
    {
        configuration.GetSection(nameof(IdentityOptions)).Bind(options);
    }
}