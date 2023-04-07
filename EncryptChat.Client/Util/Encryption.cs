using System.Security.Cryptography;
using System.Text;

namespace EncryptChat.Client.Util;

public class Encryption
{
    public static string Encrypt(string data, string key)
    {
        using var aes = Aes.Create();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        aes.Key = keyBytes;
        aes.GenerateIV();
        var iv = aes.IV;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using var sw = new StreamWriter(cs);
        sw.Write(data);
        sw.Flush();
        cs.FlushFinalBlock();
        ms.Flush();
        var encrypted = ms.ToArray();
        var result = new byte[iv.Length + encrypted.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);
        return Convert.ToBase64String(result);
    }

    public static string Decrypt(string data, string key)
    {
        using var aes = Aes.Create();
        var fullCipher = Convert.FromBase64String(data);
        var iv = new byte[aes.IV.Length];
        var cipher = new byte[fullCipher.Length - iv.Length];
        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);
        var decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), iv);
        using var ms = new MemoryStream(cipher);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}