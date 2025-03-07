using System.Runtime.InteropServices;
using XAlarm.Center.Domain.Shared;

namespace XAlarm.Center.Domain.Options;

public sealed partial class IdentityOptions
{
    [LibraryImport("ext3.so", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueLinux64(int key);

    [LibraryImport("ext3.dll", EntryPoint = "GetValue", StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string GetValueWin64(int key);

    public AuthenticationOptions AuthenticationOptions { get; init; } = new();
    public KeycloakOptions KeycloakOptions { get; init; } = new();

    public static IdentityOptions Default { get; } = new()
    {
        AuthenticationOptions = new AuthenticationOptions
        {
            Audience = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.Audience)
                : GetValueWin64((int)Constants.Audience),
            ValidIssuer = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.ValidIssuer)
                : GetValueWin64((int)Constants.ValidIssuer),
            MetadataUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.MetadataUrl)
                : GetValueWin64((int)Constants.MetadataUrl),
            RequireHttpsMetadata = false
        },
        KeycloakOptions = new KeycloakOptions
        {
            BaseUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.BaseUrl)
                : GetValueWin64((int)Constants.BaseUrl),
            AdminCliUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AdminCliUrl)
                : GetValueWin64((int)Constants.AdminCliUrl),
            GroupsUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.GroupsUrl)
                : GetValueWin64((int)Constants.GroupsUrl),
            GetUserUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.GetUserUrl)
                : GetValueWin64((int)Constants.GetUserUrl),
            GetUsersUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.GetUsersUrl)
                : GetValueWin64((int)Constants.GetUsersUrl),
            UpdateUserUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.UpdateUserUrl)
                : GetValueWin64((int)Constants.UpdateUserUrl),
            DeleteUserUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.DeleteUserUrl)
                : GetValueWin64((int)Constants.DeleteUserUrl),
            GroupMembersUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.GroupMembersUrl)
                : GetValueWin64((int)Constants.GroupMembersUrl),
            ExecuteActionsEmailUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.ExecuteActionsEmailUrl)
                : GetValueWin64((int)Constants.ExecuteActionsEmailUrl),
            AdminUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AdminUrl)
                : GetValueWin64((int)Constants.AdminUrl),
            TokenUrl = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.TokenUrl)
                : GetValueWin64((int)Constants.TokenUrl),
            AdminCliClientId = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AdminCliClientId)
                : GetValueWin64((int)Constants.AdminCliClientId),
            AdminCliClientSecret = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AdminCliClientSecret)
                : GetValueWin64((int)Constants.AdminCliClientSecret),
            AdminCliGrantType = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AdminCliGrantType)
                : GetValueWin64((int)Constants.AdminCliGrantType),
            AdminClientId = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AdminClientId)
                : GetValueWin64((int)Constants.AdminClientId),
            AdminClientSecret = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AdminClientSecret)
                : GetValueWin64((int)Constants.AdminClientSecret),
            AuthClientId = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AuthClientId)
                : GetValueWin64((int)Constants.AuthClientId),
            AuthClientSecret = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AuthClientSecret)
                : GetValueWin64((int)Constants.AuthClientSecret),
            AuthScope = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AuthScope)
                : GetValueWin64((int)Constants.AuthScope),
            AuthGrantType = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AuthGrantType)
                : GetValueWin64((int)Constants.AuthGrantType),
            AuthSubjectTokenType = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AuthSubjectTokenType)
                : GetValueWin64((int)Constants.AuthSubjectTokenType),
            AuthSubjectIssuer = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.AuthSubjectIssuer)
                : GetValueWin64((int)Constants.AuthSubjectIssuer),
            TokenGoogleClientId = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.TokenGoogleClientId)
                : GetValueWin64((int)Constants.TokenGoogleClientId),
            TokenGoogleClientSecret = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.TokenGoogleClientSecret)
                : GetValueWin64((int)Constants.TokenGoogleClientSecret),
            TokenGoogleScope = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.TokenGoogleScope)
                : GetValueWin64((int)Constants.TokenGoogleScope),
            TokenGoogleGrantType = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.TokenGoogleGrantType)
                : GetValueWin64((int)Constants.TokenGoogleGrantType),
            TokenGoogleSubjectTokenType = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.TokenGoogleSubjectTokenType)
                : GetValueWin64((int)Constants.TokenGoogleSubjectTokenType),
            TokenGoogleSubjectIssuer = OperatingSystem.IsLinux()
                ? GetValueLinux64((int)Constants.TokenGoogleSubjectIssuer)
                : GetValueWin64((int)Constants.TokenGoogleSubjectIssuer)
        }
    };
}