using System.Security.Cryptography;
using System.Text;

namespace XAlarm.Center.Shared.Helpers;

public static class EncryptHelper
{
    public static string ComputeHash(string password, string salt, string pepper, int iteration)
    {
        while (true)
        {
            if (iteration <= 0) return password;
            var passwordSaltPepper = $"{password}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
            var byteHash = SHA256.HashData(byteValue);
            var hash = Convert.ToBase64String(byteHash);
            password = hash;
            iteration -= 1;
        }
    }

    public static string GenerateSalt()
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        randomNumberGenerator.GetBytes(byteSalt);
        var salt = Convert.ToBase64String(byteSalt);
        return salt;
    }
}