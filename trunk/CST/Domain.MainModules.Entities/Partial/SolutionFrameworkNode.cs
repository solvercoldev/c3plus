using System;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Domain.MainModules.Entities
{
    public partial class TBL_Admin_OpcionesMenu
    {
        /// <summary>
        /// Create a MD5 hash of the password.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The MD5 hash of the password</returns>
        public static string HashKeyPage(string key)
        {
            if ( !string.IsNullOrEmpty(key))
            {
                return Encryption.StringToMd5Hash(key);
            }
            throw new ArgumentException("Invalid KeyPage");
        }
    }
}