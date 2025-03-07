namespace XAlarm.Center.Domain.Options;

public sealed class KeycloakOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string AdminCliUrl { get; init; } = string.Empty;
    public string GetUserUrl { get; init; } = string.Empty;
    public string GetUsersUrl { get; init; } = string.Empty;
    public string UpdateUserUrl { get; init; } = string.Empty;
    public string DeleteUserUrl { get; init; } = string.Empty;
    public string GroupsUrl { get; init; } = string.Empty;
    public string GroupMembersUrl { get; init; } = string.Empty;
    public string ExecuteActionsEmailUrl { get; init; } = string.Empty;
    public string AdminUrl { get; init; } = string.Empty;
    public string TokenUrl { get; init; } = string.Empty;
    public string AdminCliClientId { get; init; } = string.Empty;
    public string AdminCliClientSecret { get; init; } = string.Empty;
    public string AdminCliGrantType { get; init; } = string.Empty;
    public string AdminClientId { get; init; } = string.Empty;
    public string AdminClientSecret { get; init; } = string.Empty;
    public string AuthClientId { get; init; } = string.Empty;
    public string AuthClientSecret { get; init; } = string.Empty;
    public string AuthScope { get; init; } = string.Empty;
    public string AuthGrantType { get; init; } = string.Empty;
    public string AuthSubjectTokenType { get; init; } = string.Empty;
    public string AuthSubjectIssuer { get; init; } = string.Empty;
    public string TokenGoogleClientId { get; init; } = string.Empty;
    public string TokenGoogleClientSecret { get; init; } = string.Empty;
    public string TokenGoogleScope { get; init; } = string.Empty;
    public string TokenGoogleGrantType { get; init; } = string.Empty;
    public string TokenGoogleSubjectTokenType { get; init; } = string.Empty;
    public string TokenGoogleSubjectIssuer { get; init; } = string.Empty;
}