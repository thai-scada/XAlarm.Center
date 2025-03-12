using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;
using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Projects;
using XAlarm.Center.Domain.Shared;
using XAlarm.Center.Infrastructure.IdentityServer.Abstractions;
using XAlarm.Center.Infrastructure.IdentityServer.Models;
using XAlarm.Center.Shared.Helpers;

namespace XAlarm.Center.Infrastructure.IdentityServer;

internal sealed partial class AuthenticationService(
    HttpClient httpClient,
    IOptions<AppOptions> appOptions,
    IOptions<IdentityOptions> identityOptions) : IAuthenticationService
{
    [LibraryImport("ext3.so", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueLinux64(int key);

    [LibraryImport("ext3.dll", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueWin64(int key);

    private static readonly Error InvalidProjectId =
        new("Keycloak.InvalidProjectId", "Project id is invalid. Please contact support team.");

    private static readonly Error MaximumNumberUsersExceeded =
        new("Keycloak.MaximumNumberUsersExceeded", "You have reached the maximum number of users");

    private static readonly Error CreateUserFailed = new("Keycloak.CreateUserFailed", "Failed to create user");
    private static readonly Error GetAccountFailed = new("Keycloak.GetAccountFailed", "Failed to get user");

    private static readonly Error EmailNotFound = new("Keycloak.EmailNotFound", "Email not found");

    private static readonly Error EmailNotVerified = new("Keycloak.EmailNotVerified", "Email is not verified");

    private readonly AppOptions _appOptions = appOptions.Value;
    private readonly KeycloakOptions _keycloakOptions = identityOptions.Value.KeycloakOptions;

    public async Task<Result<string>> CreateAccountAsync(string email, string firstName, string lastName,
        Project project, CancellationToken cancellationToken = default)
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

            var defaultGroup = $"xalarm-{project.ProjectName}";

            if (email.Contains("thaiscada", StringComparison.CurrentCultureIgnoreCase))
                defaultGroup = "xalarm-administrator";
            else
            {
                var groups =
                    await httpClient.GetFromJsonAsync<GroupProfile[]>(_keycloakOptions.GroupsUrl, cancellationToken);
                var group = groups?.FirstOrDefault(x => x.Id == project.ProjectId.ToString());

                if (group is not null)
                {
                    var groupMembers = await httpClient.GetFromJsonAsync<UserProfile[]>(
                        _keycloakOptions.GroupMembersUrl.Replace("{groupId}", group.Id), cancellationToken);

                    if (groupMembers?.Length >= int.Parse(OperatingSystem.IsLinux()
                            ? GetValueLinux64((int)Constants.MaxUsers)
                            : GetValueWin64((int)Constants.MaxUsers)))
                        return Result.Failure<string>(MaximumNumberUsersExceeded);
                }
                else
                    return Result.Failure<string>(InvalidProjectId);
            }

            var userRepresentation = new UserRepresentation
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Enabled = true,
                Groups = [defaultGroup],
                RequiredActions = ["VERIFY_EMAIL", "UPDATE_PASSWORD"]
            };

            response = await httpClient.PostAsJsonAsync(_keycloakOptions.AdminUrl, userRepresentation,
                JsonHelper.DefaultJsonSerializerOptions, cancellationToken);

            response.EnsureSuccessStatusCode();

            var userProfiles = await httpClient.GetFromJsonAsync<UserProfile[]>(
                _keycloakOptions.GetUsersUrl.Replace("{username}", email), cancellationToken);

            if (!(userProfiles?.Length > 0))
                return Result.Failure<string>(CreateUserFailed);

            var userId = userProfiles.First().Id.ToString();
            response = await httpClient.PutAsJsonAsync(
                _keycloakOptions.ExecuteActionsEmailUrl.Replace("{userId}", userId)
                    .Replace("{redirectUri}", _appOptions.AppUrl.UrlLoopback),
                (string[]) ["VERIFY_EMAIL", "UPDATE_PASSWORD"], cancellationToken);

            response.EnsureSuccessStatusCode();

            return userId;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(CreateUserFailed);
        }
    }

    public async Task<Result<string>> GetAccountAsync(string email, CancellationToken cancellationToken = default)
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

            return userProfiles?.Length > 0
                ? userProfiles[0].EmailVerified ? email : Result.Failure<string>(EmailNotVerified)
                : Result.Failure<string>(EmailNotFound);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(GetAccountFailed);
        }
    }
}