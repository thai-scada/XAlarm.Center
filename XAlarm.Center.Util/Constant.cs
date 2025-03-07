using System.Runtime.InteropServices;

namespace XAlarm.Center.Util;

public class Constant
{
    private const string CredentialId = "admin";
    private const string CredentialSecret = "Wththaiscada2468!";
    private const string MaxUsers = "5";
    private const string Audience = "account";
    private const string ValidIssuer = "https://auth.iotserver.in.th/realms/xalarm";
    private const string MetadataUrl = "https://auth.iotserver.in.th/realms/xalarm/.well-known/openid-configuration";
    private const string BaseUrl = "https://auth.iotserver.in.th";
    private const string AdminCliUrl = "https://auth.iotserver.in.th/realms/master/protocol/openid-connect/token";
    private const string GroupsUrl = "https://auth.iotserver.in.th/admin/realms/xalarm/groups";
    private const string GetUserUrl = "https://auth.iotserver.in.th/admin/realms/xalarm/users/{userId}";
    private const string GetUsersUrl = "https://auth.iotserver.in.th/admin/realms/xalarm/users?username={username}";
    private const string UpdateUserUrl = "https://auth.iotserver.in.th/admin/realms/xalarm/users/{userId}";
    private const string DeleteUserUrl = "https://auth.iotserver.in.th/admin/realms/xalarm/users/{userId}";
    private const string GroupMembersUrl = "https://auth.iotserver.in.th/admin/realms/xalarm/groups/{groupId}/members";

    private const string ExecuteActionsEmailUrl =
        "https://auth.iotserver.in.th/admin/realms/xalarm/users/{userId}/execute-actions-email?client_id=xalarm-admin-client&redirect_uri={redirectUri}";

    private const string AdminUrl = "https://auth.iotserver.in.th/admin/realms/xalarm/users";
    private const string TokenUrl = "https://auth.iotserver.in.th/realms/xalarm/protocol/openid-connect/token";
    private const string AdminCliClientId = "admin-cli";
    private const string AdminCliClientSecret = "";
    private const string AdminCliGrantType = "password";
    private const string AdminClientId = "xalarm-admin-client";
    private const string AdminClientSecret = "PVA0SsK2FrAxoPm0IiABQ0XYqhJlgt9L";
    private const string AuthClientId = "xalarm-auth-client";
    private const string AuthClientSecret = "DRGFxhOjnzm66e57qmrCxyOC1GDw5fHe";
    private const string AuthScope = "openid email";
    private const string AuthGrantType = "password";
    private const string AuthSubjectTokenType = "";
    private const string AuthSubjectIssuer = "";
    private const string TokenGoogleClientId = "xalarm-token-google-client";
    private const string TokenGoogleClientSecret = "ofCAzaWmyqd9dGwl6Y70jjfs1GMYT7TB";
    private const string TokenGoogleScope = "";
    private const string TokenGoogleGrantType = "urn:ietf:params:oauth:grant-type:token-exchange";
    private const string TokenGoogleSubjectTokenType = "urn:ietf:params:oauth:token-type:access_token";
    private const string TokenGoogleSubjectIssuer = "google";

    [UnmanagedCallersOnly(EntryPoint = "GetValue")]
    public static IntPtr GetValue(int key)
    {
        return key switch
        {
            1 => Marshal.StringToHGlobalUni(CredentialId),
            2 => Marshal.StringToHGlobalUni(CredentialSecret),
            3 => Marshal.StringToHGlobalUni(MaxUsers),
            4 => Marshal.StringToHGlobalUni(Audience),
            5 => Marshal.StringToHGlobalUni(ValidIssuer),
            6 => Marshal.StringToHGlobalUni(MetadataUrl),
            7 => Marshal.StringToHGlobalUni(BaseUrl),
            8 => Marshal.StringToHGlobalUni(AdminCliUrl),
            9 => Marshal.StringToHGlobalUni(GroupsUrl),
            10 => Marshal.StringToHGlobalUni(GetUserUrl),
            11 => Marshal.StringToHGlobalUni(GetUsersUrl),
            12 => Marshal.StringToHGlobalUni(UpdateUserUrl),
            13 => Marshal.StringToHGlobalUni(DeleteUserUrl),
            14 => Marshal.StringToHGlobalUni(GroupMembersUrl),
            15 => Marshal.StringToHGlobalUni(ExecuteActionsEmailUrl),
            16 => Marshal.StringToHGlobalUni(AdminUrl),
            17 => Marshal.StringToHGlobalUni(TokenUrl),
            18 => Marshal.StringToHGlobalUni(AdminCliClientId),
            19 => Marshal.StringToHGlobalUni(AdminCliClientSecret),
            20 => Marshal.StringToHGlobalUni(AdminCliGrantType),
            21 => Marshal.StringToHGlobalUni(AdminClientId),
            22 => Marshal.StringToHGlobalUni(AdminClientSecret),
            23 => Marshal.StringToHGlobalUni(AuthClientId),
            24 => Marshal.StringToHGlobalUni(AuthClientSecret),
            25 => Marshal.StringToHGlobalUni(AuthScope),
            26 => Marshal.StringToHGlobalUni(AuthGrantType),
            27 => Marshal.StringToHGlobalUni(AuthSubjectTokenType),
            28 => Marshal.StringToHGlobalUni(AuthSubjectIssuer),
            29 => Marshal.StringToHGlobalUni(TokenGoogleClientId),
            30 => Marshal.StringToHGlobalUni(TokenGoogleClientSecret),
            31 => Marshal.StringToHGlobalUni(TokenGoogleScope),
            32 => Marshal.StringToHGlobalUni(TokenGoogleGrantType),
            33 => Marshal.StringToHGlobalUni(TokenGoogleSubjectTokenType),
            34 => Marshal.StringToHGlobalUni(TokenGoogleSubjectIssuer),
            _ => Marshal.StringToHGlobalUni(string.Empty)
        };
    }
}