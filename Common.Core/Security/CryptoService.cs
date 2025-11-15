using System.Security.Cryptography;
using System.Text;

namespace Common.Core.Security
{
    public interface ICryptoService
    {
        // Hashing
        string ComputeSha256Hash(string input);
        string ComputeSha512Hash(string input);

        // HMAC
        string ComputeHmacSha256(string key, string message);

        // Symmetric encryption (AES)
        string EncryptString(string plainText, string key);
        string DecryptString(string cipherText, string key);

        // Random secure token
        string GenerateSecureToken(int length = 32);
    }

    public class CryptoService : ICryptoService
    {
        // --- Hashing ---
        public string ComputeSha256Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }

        public string ComputeSha512Hash(string input)
        {
            using var sha512 = SHA512.Create();
            var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }

        // --- HMAC ---
        public string ComputeHmacSha256(string key, string message)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return Convert.ToHexString(bytes);
        }

        // --- AES Encryption ---
        public string EncryptString(string plainText, string key)
        {
            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);
            sw.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptString(string cipherText, string key)
        {
            var bytes = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));

            var iv = new byte[aes.BlockSize / 8];
            Array.Copy(bytes, 0, iv, 0, iv.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(bytes, iv.Length, bytes.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        // --- Secure Token ---
        public string GenerateSecureToken(int length = 32)
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToHexString(tokenBytes);
        }
    }
}
