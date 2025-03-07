using Microsoft.AspNetCore.Authorization;
using XAlarm.Center.Domain.Users;

namespace XAlarm.Center.Infrastructure.IdentityServer;

public sealed class HasRoleAttribute(RoleTypes role) : AuthorizeAttribute(role.ToString());