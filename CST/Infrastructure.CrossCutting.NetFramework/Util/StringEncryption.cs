using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public static class StringEncryption
    {
        public static readonly int KeyLengthBits = 256; //AES Key Length in bits
        public static readonly int SaltLength = 8; //Salt length in bytes
        private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public static string DecryptString(string ciphertext, string passphrase)
        {
            var inputs = ciphertext.Split(':');
            var iv = Convert.FromBase64String(inputs[0]); // Extract the IV
            var salt = Convert.FromBase64String(inputs[1]); // Extract the salt
            var ciphertextBytes = Convert.FromBase64String(inputs[2]); // Extract the ciphertext

            // Derive the key from the supplied passphrase and extracted salt
            byte[] key = DeriveKeyFromPassphrase(passphrase, salt);

            // Decrypt
            byte[] plaintext = DoCryptoOperation(ciphertextBytes, key, iv, false);

            // Return the decrypted string
            return Encoding.UTF8.GetString(plaintext);
        }

        public static string EncryptString(string plaintext, string passphrase)
        {
            var salt = GenerateRandomBytes(SaltLength); // Random salt
            var iv = GenerateRandomBytes(16); // AES is always a 128-bit block size
            var key = DeriveKeyFromPassphrase(passphrase, salt); // Derive the key from the passphrase

            // Encrypt
            var ciphertext = DoCryptoOperation(Encoding.UTF8.GetBytes(plaintext), key, iv, true);

            // Return the formatted string
            return String.Format("{0}:{1}:{2}", Convert.ToBase64String(iv), Convert.ToBase64String(salt), Convert.ToBase64String(ciphertext));
        }

        private static byte[] DeriveKeyFromPassphrase(string passphrase, byte[] salt, int iterationCount = 2000)
        {
            var keyDerivationFunction = new Rfc2898DeriveBytes(passphrase, salt, iterationCount);  //PBKDF2

            return keyDerivationFunction.GetBytes(KeyLengthBits / 8);
        }

        private static byte[] GenerateRandomBytes(int lengthBytes)
        {
            var bytes = new byte[lengthBytes];
            rng.GetBytes(bytes);

            return bytes;
        }

        // This function does both encryption and decryption, depending on the value of the "encrypt" parameter
        private static byte[] DoCryptoOperation(byte[] inputData, byte[] key, byte[] iv, bool encrypt)
        {
            byte[] output;

            using (var aes = new AesCryptoServiceProvider())
            using (var ms = new MemoryStream())
            {
                var cryptoTransform = encrypt ? aes.CreateEncryptor(key, iv) : aes.CreateDecryptor(key, iv);

                using (var cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                    cs.Write(inputData, 0, inputData.Length);

                output = ms.ToArray();
            }

            return output;
        }
    }
}