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

internal sealed partial class UserProfileService(HttpClient httpClient, IOptions<IdentityOptions> identityOptions)
    : IUserProfileService
{
    [LibraryImport("ext3.so", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueLinux64(int key);

    [LibraryImport("ext3.dll", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueWin64(int key);

    private static readonly Error GetUserProfileFailed =
        new("Keycloak.GetUserProfileFailed", "Failed to get user profile");

    private static readonly Error UpdateUserProfileFailed =
        new("Keycloak.UpdateUserProfileFailed", "Failed to update user profile");

    private static readonly Error DeleteUserFailed = new("Keycloak.DeleteUserFailed", "Failed to delete user");

    private static readonly Error EmailNotFound = new("Keycloak.EmailNotFound", "Email not found");

    private readonly KeycloakOptions _keycloakOptions = identityOptions.Value.KeycloakOptions;

    public async Task<Result<PersonalInfo>> GetUserProfileAsync(string userId,
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
                await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken: cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authorizationToken?.AccessToken);

            var personalInfo =
                await httpClient.GetFromJsonAsync<PersonalInfo>(_keycloakOptions.GetUserUrl.Replace("{userId}", userId),
                    cancellationToken);

            return personalInfo ?? Result.Failure<PersonalInfo>(EmailNotFound);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<PersonalInfo>(GetUserProfileFailed);
        }
    }

    public async Task<Result<string>> UpdateUserProfileAsync(PersonalInfo personalInfo,
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
                await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken: cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authorizationToken?.AccessToken);

            var userRepresentation = new { personalInfo.FirstName, personalInfo.LastName };

            response = await httpClient.PutAsJsonAsync(
                _keycloakOptions.UpdateUserUrl.Replace("{userId}", personalInfo.Id.ToString()), userRepresentation,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return personalInfo.Id.ToString();
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(UpdateUserProfileFailed);
        }
    }

    public async Task<Result<string>> DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
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
                await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken: cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authorizationToken?.AccessToken);

            response = await httpClient.DeleteAsync(
                _keycloakOptions.DeleteUserUrl.Replace("{userId}", userId), cancellationToken);

            response.EnsureSuccessStatusCode();

            return userId;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(DeleteUserFailed);
        }
    }
}