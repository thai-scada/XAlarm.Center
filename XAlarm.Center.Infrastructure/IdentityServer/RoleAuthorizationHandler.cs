using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using XAlarm.Center.Domain.Shared;
using XAlarm.Center.Shared.Helpers;

namespace XAlarm.Center.Infrastructure.IdentityServer;

public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        var realmAccessRaw = context.User.Claims.FirstOrDefault(x => x.Type == "realm_access")?.Value;

        if (realmAccessRaw is null) return Task.CompletedTask;
        var realmAccess =
            JsonSerializer.Deserialize<RealmAccess>(realmAccessRaw, JsonHelper.DefaultJsonSerializerOptions);
        if (realmAccess is not null && realmAccess.Roles.Contains(requirement.Role))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}