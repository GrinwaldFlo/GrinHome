using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace GrinHome.Data
{
    public static class Crypto
    {
        //private static string Key { get; set; } = "";
        private static ICryptoTransform? encryptor;
        private static ICryptoTransform? decryptor;

        public static bool SetKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            try
            {
                Aes aes = Aes.Create();

                aes.Key = Convert.FromBase64String(key);
                aes.IV = new byte[16];

                encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                var dummyEn = EncryptString("dummy");
                var test = DecryptString(dummyEn);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Check key", ex);
                return false;
            }
        }

        public static void CreateKey()
        {
            Log.Information("Key is invalid, the following generated key is used now");
            Aes aes = Aes.Create();
            aes.GenerateIV();
            aes.GenerateKey();

            Log.Information("New key has been generated at root of the application key.txt, copy it to you application settings");
            File.WriteAllText("key.txt", Convert.ToBase64String(aes.Key));

            SetKey(Convert.ToBase64String(aes.Key));
        }

        public static string EncryptString(string plainText)
        {
            if (encryptor == null)
                return "";
            byte[] plaintext = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherText = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);

            return Convert.ToBase64String(cipherText);
        }

        public static string DecryptString(string cipherText)
        {
            if (decryptor == null)
                return "";
            byte[] encryptedBytes = Convert.FromBase64String(cipherText);
            byte[] plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }


        public static string Encrypt(this String str)
        {
            return EncryptString(str);
        }

        public static string Decrypt(this String str)
        {
            return DecryptString(str);
        }
    }
}
