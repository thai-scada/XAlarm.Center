using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Domain.Shared;

public sealed class PersonalInfo : Entity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}