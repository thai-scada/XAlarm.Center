using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;
using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Shared;
using XAlarm.Center.Infrastructure.IdentityServer.Abstractions;
using XAlarm.Center.Infrastructure.IdentityServer.Models;

namespace XAlarm.Center.Infrastructure.IdentityServer;

internal sealed partial class JwtService(
    HttpClient httpClient,
    IOptions<ProjectOptions> projectOptions,
    IOptions<IdentityOptions> identityOptions) : IJwtService
{
    [LibraryImport("ext3.so", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueLinux64(int key);

    [LibraryImport("ext3.dll", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueWin64(int key);

    private static readonly Error InvalidProjectId =
        new("Keycloak.InvalidProjectId", "Project id is invalid. Please contact support team.");

    private static readonly Error InvalidUserCredentials =
        new("Keycloak.InvalidUserCredentials", "Invalid user credentials");

    private static readonly Error AuthenticationFailed = new("Keycloak.AuthenticationFailed",
        "Failed to acquire access token due to authentication failure");

    private readonly KeycloakOptions _keycloakOptions = identityOptions.Value.KeycloakOptions;
    private readonly ProjectOptions _projectOptions = projectOptions.Value;

    public async Task<Result<TokenInfo>> GetAccessTokenAsync(string email, string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("scope", _keycloakOptions.AuthScope),
                new("grant_type", _keycloakOptions.AuthGrantType),
                new("username", email),
                new("password", password)
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await httpClient.PostAsync(_keycloakOptions.TokenUrl, authorizationRequestContent,
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                if (!email.Contains("thaiscada", StringComparison.CurrentCultureIgnoreCase))
                {
                    var adminCliAuthRequestParameters = new KeyValuePair<string, string>[]
                    {
                        new("client_id", _keycloakOptions.AdminCliClientId),
                        new("grant_type", _keycloakOptions.AdminCliGrantType),
                        new("username", OperatingSystem.IsLinux()
                            ? GetValueLinux64((int)Constants.CredentialId)
                            : GetValueWin64((int)Constants.CredentialId)),
                        new("password", OperatingSystem.IsLinux()
                            ? GetValueLinux64((int)Constants.CredentialSecret)
                            : GetValueWin64((int)Constants.CredentialSecret))
                    };

                    var adminCliAuthorizationRequestContent = new FormUrlEncodedContent(adminCliAuthRequestParameters);

                    var adminCliResponse = await httpClient.PostAsync(_keycloakOptions.AdminCliUrl,
                        adminCliAuthorizationRequestContent,
                        cancellationToken);

                    adminCliResponse.EnsureSuccessStatusCode();

                    var adminCliAuthorizationToken =
                        await adminCliResponse.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", adminCliAuthorizationToken?.AccessToken);

                    var groups =
                        await httpClient.GetFromJsonAsync<GroupProfile[]>(_keycloakOptions.GroupsUrl,
                            cancellationToken);
                    var group = groups?.FirstOrDefault(x => x.Id == _projectOptions.Id);

                    if (group is null)
                        return Result.Failure<TokenInfo>(InvalidProjectId);

                    var groupMembers = await httpClient.GetFromJsonAsync<UserProfile[]>(
                        _keycloakOptions.GroupMembersUrl.Replace("{groupId}", group.Id), cancellationToken);

                    if (groupMembers?.FirstOrDefault(x => x.Username == email) is null)
                        return Result.Failure<TokenInfo>(InvalidUserCredentials);
                }

                var authorizationToken =
                    await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

                return authorizationToken is not null
                    ? new TokenInfo
                        { AccessToken = authorizationToken.AccessToken, RefreshToken = authorizationToken.RefreshToken }
                    : Result.Failure<TokenInfo>(AuthenticationFailed);
            }

            var authorizationResult =
                await response.Content.ReadFromJsonAsync<AuthorizationResult>(cancellationToken);

            return Result.Failure<TokenInfo>(new Error(authorizationResult?.Error ?? string.Empty,
                authorizationResult?.ErrorDescription ?? string.Empty));
        }
        catch (HttpRequestException)
        {
            return Result.Failure<TokenInfo>(AuthenticationFailed);
        }
    }

    public async Task<Result<TokenInfo>> RefreshAccessTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("scope", _keycloakOptions.AuthScope),
                new("grant_type", "refresh_token"),
                new("refresh_token", refreshToken)
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await httpClient.PostAsync(_keycloakOptions.TokenUrl, authorizationRequestContent,
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var authorizationToken =
                    await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

                return authorizationToken is not null
                    ? new TokenInfo
                        { AccessToken = authorizationToken.AccessToken, RefreshToken = authorizationToken.RefreshToken }
                    : Result.Failure<TokenInfo>(AuthenticationFailed);
            }

            var authorizationResult =
                await response.Content.ReadFromJsonAsync<AuthorizationResult>(cancellationToken);

            return Result.Failure<TokenInfo>(new Error(authorizationResult?.Error ?? string.Empty,
                authorizationResult?.ErrorDescription ?? string.Empty));
        }
        catch (HttpRequestException)
        {
            return Result.Failure<TokenInfo>(AuthenticationFailed);
        }
    }
}