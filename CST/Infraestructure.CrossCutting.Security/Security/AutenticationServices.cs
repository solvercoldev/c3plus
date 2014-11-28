using System.Web;
using Domain.MainModules.Entities;
using Infraestructure.CrossCutting.Security.IServices;

namespace Infraestructure.CrossCutting.Security.Security
{
    public class AutenticationServices : IAutentication
    {
        public bool ValidarAutorizacion(string className)
        {
            return true;
        }

        public TBL_Admin_Usuarios GetUserFromSession
        {
             get { return ((TBL_Admin_Usuarios)HttpContext.Current.User.Identity); }
        }

       
    }
}