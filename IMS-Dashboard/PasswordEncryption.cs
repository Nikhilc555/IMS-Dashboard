using System.Security.Cryptography;
using System.Text;

namespace IMS_Dashboard
{
    public class PasswordEncryption
    {
        private static readonly string EncryptionKey = "lihyfvooIUHVjhvouyfv987Y69G73RBJCSXCV87VJHV8FC7TAFBUJjvhjzcg8gasbdoaGG9GvO8VGc87g8v8gGCV78!"; // Change this key

        public static string Encrypt(string plainText)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x20, 0x76, 0x4f, 0x6d });
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x20, 0x76, 0x4f, 0x6d });
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.FlushFinalBlock();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
