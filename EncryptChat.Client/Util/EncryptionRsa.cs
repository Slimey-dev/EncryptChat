using System.Security.Cryptography;
using System.Text;

namespace EncryptChat.Client.Util;

public class EncryptionRsa
{
    public static string Encrypt(string data, byte[] key)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(key, out _);
        var encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.OaepSHA1);
        return Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(string data, byte[] key)
    {
        using var rsa = RSA.Create();

        rsa.ImportRSAPrivateKey(key, out _);
        var decrypted = rsa.Decrypt(Convert.FromBase64String(data), RSAEncryptionPadding.OaepSHA1);
        return Encoding.UTF8.GetString(decrypted);
    }

    public static string GenerateKey()
    {
        using var rsa = RSA.Create();
        return Convert.ToBase64String(rsa.ExportRSAPublicKey());
    }
}