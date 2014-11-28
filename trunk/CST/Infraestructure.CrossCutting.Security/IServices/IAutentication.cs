using Domain.MainModules.Entities;

namespace Infraestructure.CrossCutting.Security.IServices
{
    public interface IAutentication
    {
       
        bool ValidarAutorizacion(string className);

        TBL_Admin_Usuarios GetUserFromSession { get; }
    }
}