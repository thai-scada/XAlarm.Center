namespace XAlarm.Center.Infrastructure.IdentityServer.Models;

public sealed class UserRepresentation
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool? Enabled { get; set; }
    public string[] Groups { get; set; } = [];
    public string[] RequiredActions { get; set; } = [];
}