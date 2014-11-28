using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public class Encryption
    {
        /// <summary>
        /// Calculates the MD5 of a given string.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>The (hexadecimal) string representatation of the MD5 hash.</returns>
        public static string StringToMd5Hash(string inputString)
        {
            var md5 = new MD5CryptoServiceProvider();
            var encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            var sb = new StringBuilder();
            foreach (var t in encryptedBytes)
            {
                sb.AppendFormat("{0:x2}", t);
            }
            return sb.ToString();
        }
    }
}