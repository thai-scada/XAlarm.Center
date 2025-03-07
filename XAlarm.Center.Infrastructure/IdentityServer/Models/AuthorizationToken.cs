using System.Text.Json.Serialization;

namespace XAlarm.Center.Infrastructure.IdentityServer.Models;

public sealed class AuthorizationToken
{
    [JsonPropertyName("access_token")] public string AccessToken { get; init; } = string.Empty;
    [JsonPropertyName("refresh_token")] public string RefreshToken { get; init; } = string.Empty;
}