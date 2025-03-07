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

internal sealed partial class ExecuteActionsEmailService(
    HttpClient httpClient,
    IOptions<AppOptions> appOptions,
    IOptions<IdentityOptions> identityOptions)
    : IExecuteActionsEmailService
{
    [LibraryImport("ext3.so", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueLinux64(int key);

    [LibraryImport("ext3.dll", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueWin64(int key);

    private static readonly Error ExecuteAccountNotFullySetUpFailed = new("Keycloak.ExecuteAccountNotFullySetUpFailed",
        "Failed to execute actions email 'Account is not fully set up'");

    private static readonly Error ExecuteForgotPasswordFailed = new("Keycloak.ExecuteForgotPasswordFailed",
        "Failed to execute actions email 'Forgot password'");

    private readonly AppOptions _appOptions = appOptions.Value;
    private readonly KeycloakOptions _keycloakOptions = identityOptions.Value.KeycloakOptions;

    public async Task<Result<string>> ExecuteAccountNotFullySetUpAsync(string email,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
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

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await httpClient.PostAsync(_keycloakOptions.AdminCliUrl, authorizationRequestContent,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken =
                await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authorizationToken?.AccessToken);

            var userProfiles = await httpClient.GetFromJsonAsync<UserProfile[]>(
                _keycloakOptions.GetUsersUrl.Replace("{username}", email), cancellationToken);

            if (!(userProfiles?.Length > 0))
                return Result.Failure<string>(ExecuteAccountNotFullySetUpFailed);

            var userId = userProfiles.First().Id.ToString();
            response = await httpClient.PutAsJsonAsync(
                _keycloakOptions.ExecuteActionsEmailUrl.Replace("{userId}", userId)
                    .Replace("{redirectUri}", _appOptions.AppUrls.UrlLoopback),
                (string[]) ["VERIFY_EMAIL", "UPDATE_PASSWORD"], cancellationToken);

            response.EnsureSuccessStatusCode();

            return userId;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(ExecuteAccountNotFullySetUpFailed);
        }
    }

    public async Task<Result<string>> ExecuteForgotPasswordAsync(string email,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
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

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await httpClient.PostAsync(_keycloakOptions.AdminCliUrl, authorizationRequestContent,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken =
                await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authorizationToken?.AccessToken);

            var userProfiles = await httpClient.GetFromJsonAsync<UserProfile[]>(
                _keycloakOptions.GetUsersUrl.Replace("{username}", email), cancellationToken);

            if (!(userProfiles?.Length > 0))
                return Result.Failure<string>(new Error("Keycloak.ExecuteForgotPasswordEmailNotFound",
                    $"This email address {email} does not exist."));

            var userId = userProfiles.First().Id.ToString();
            response = await httpClient.PutAsJsonAsync(
                _keycloakOptions.ExecuteActionsEmailUrl.Replace("{userId}", userId)
                    .Replace("{redirectUri}", _appOptions.AppUrls.UrlLoopback), (string[]) ["UPDATE_PASSWORD"],
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return userId;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(ExecuteForgotPasswordFailed);
        }
    }
}