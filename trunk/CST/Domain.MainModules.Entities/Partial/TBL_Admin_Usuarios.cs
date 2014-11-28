using System;
using System.Linq;
using System.Security.Principal;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Domain.MainModules.Entities
{
    public partial class TBL_Admin_Usuarios : IIdentity
    {
        private static bool ValidatePassword(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            return password.Length > 5;
        }

        public string Name
        {
            get
            {
                return IsAuthenticated ? Nombres : string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AuthenticationType
        {
            get { return "SolutionFrameworkAuthetication"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual bool IsInRole(string roleName)
        {
            return _tBL_Admin_Roles.Any(x => x.NombreRol.Equals(roleName));
        }

        public bool IsInRoleId(int idRol)
        {
            return _tBL_Admin_Roles.Any(x => x.IdRol == idRol);
        }

        /// <summary>
        /// Create a MD5 hash of the password.
        /// </summary>
        /// <param name="password">The password in clear text</param>
        /// <returns>The MD5 hash of the password</returns>
        public static string HashPassword(string password)
        {
            if (ValidatePassword(password))
            {
                return Encryption.StringToMd5Hash(password);
            }
            throw new ArgumentException("Invalid password");
        }
    }
}