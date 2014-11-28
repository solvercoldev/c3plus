
using System.Linq;
using System.Security;
using System.Security.Principal;
using Domain.MainModules.Entities;

namespace Infraestructure.CrossCutting.Security.Security
{
    public class SolutionFrameworkPrincipal : IPrincipal
    {
        private readonly TBL_Admin_Usuarios _user;

        public SolutionFrameworkPrincipal(TBL_Admin_Usuarios user)
        {
            if (user != null && user.IsAuthenticated)
            {
                _user = user;
            }
            else
            {
                throw new SecurityException("No se puede crear un usuario Valido.");
            }
        }

        public TBL_Admin_Usuarios GetUser
        {
            get { return _user; }
        }

        public bool IsInRoleLabel(string role)
        {
            return _user.TBL_Admin_Roles.Any(x => x.NombreRol.Equals(role));
        }

        public bool IsInRole(string role)
        {
            return _user.TBL_Admin_Roles.Any(x => x.NombreRol.Equals(role));
        }

        public IIdentity Identity
        {
            get { return _user; }
        }
    }
}
