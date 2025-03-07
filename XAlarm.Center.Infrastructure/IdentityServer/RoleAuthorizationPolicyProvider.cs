using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace XAlarm.Center.Infrastructure.IdentityServer;

public class RoleAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        return policy ?? new AuthorizationPolicyBuilder().AddRequirements(new RoleRequirement(policyName)).Build();
    }
}