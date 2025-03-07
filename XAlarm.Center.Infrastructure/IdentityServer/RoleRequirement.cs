using Microsoft.AspNetCore.Authorization;

namespace XAlarm.Center.Infrastructure.IdentityServer;

public class RoleRequirement(string role) : IAuthorizationRequirement
{
    public string Role { get; } = role;
}