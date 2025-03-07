using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Infrastructure.IdentityServer.Abstractions;
using XAlarm.Center.Infrastructure.IdentityServer.Models;

namespace XAlarm.Center.Infrastructure.IdentityServer;

internal sealed class JwtExchangeService(HttpClient httpClient, IOptions<IdentityOptions> identityOptions)
    : IJwtServiceExchange
{
    private static readonly Error AuthenticationFailed = new("Keycloak.AuthenticationFailed",
        "Failed to acquire access token due to authentication failure");

    private readonly KeycloakOptions _keycloakOptions = identityOptions.Value.KeycloakOptions;

    public async Task<Result<string>> GetAccessTokenExchangeAsync(string token,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.TokenGoogleClientId),
                new("client_secret", _keycloakOptions.TokenGoogleClientSecret),
                new("grant_type", _keycloakOptions.TokenGoogleGrantType),
                new("subject_token_type", _keycloakOptions.TokenGoogleSubjectTokenType),
                new("subject_issuer", _keycloakOptions.TokenGoogleSubjectIssuer)
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await httpClient.PostAsync(_keycloakOptions.TokenUrl, authorizationRequestContent,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken =
                await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            return authorizationToken?.AccessToken ?? Result.Failure<string>(AuthenticationFailed);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(AuthenticationFailed);
        }
    }
}