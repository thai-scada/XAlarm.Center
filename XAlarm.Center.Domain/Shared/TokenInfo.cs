using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Domain.Shared;

public sealed class TokenInfo : Entity
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
}