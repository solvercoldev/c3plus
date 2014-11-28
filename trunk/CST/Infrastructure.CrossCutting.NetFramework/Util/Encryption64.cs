using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public class Cryptography
    {
        #region Fields

        private static byte[] _key = { };
        private static readonly byte[] Iv = { 38, 55, 206, 48, 28, 64, 20, 16 };
        private const string StringKey = "!5663a#KN";

        #endregion

        #region Public Methods

        public static string Encrypt(string text)
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(StringKey.Substring(0, 8));

                var des = new DESCryptoServiceProvider();
                var byteArray = Encoding.UTF8.GetBytes(text);

                var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream,des.CreateEncryptor(_key, Iv), CryptoStreamMode.Write);
                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw  new Exception("error al encriptar",ex);
            }

        }

        public static string Decrypt(string text)
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(StringKey.Substring(0, 8));

                var des = new DESCryptoServiceProvider();
                var byteArray = Convert.FromBase64String(text);

                var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream,des.CreateDecryptor(_key, Iv), CryptoStreamMode.Write);
                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("error al desencriptar", ex);
            }

          
        }

        #endregion
    }
}