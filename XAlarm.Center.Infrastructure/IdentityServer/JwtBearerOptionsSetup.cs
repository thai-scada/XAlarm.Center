using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Infrastructure.IdentityServer;

internal sealed class JwtBearerOptionsSetup(IOptions<IdentityOptions> identityOptions)
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions = identityOptions.Value.AuthenticationOptions;

    public void Configure(JwtBearerOptions options)
    {
        options.Audience = _authenticationOptions.Audience;
        options.MetadataAddress = _authenticationOptions.MetadataUrl;
        options.RequireHttpsMetadata = _authenticationOptions.RequireHttpsMetadata;
        options.TokenValidationParameters.ValidIssuer = _authenticationOptions.ValidIssuer;
        options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}