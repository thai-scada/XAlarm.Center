using System.Text.Json.Serialization;

namespace XAlarm.Center.Infrastructure.IdentityServer.Models;

public sealed class AuthorizationResult
{
    [JsonPropertyName("error")] public string Error { get; init; } = string.Empty;

    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; init; } = string.Empty;
}