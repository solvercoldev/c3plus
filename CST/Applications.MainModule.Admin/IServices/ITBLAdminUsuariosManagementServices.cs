//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por una plantilla T4 de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  Abril 24 de 2012.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using Domain.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Applications.MainModule.Admin.IServices
{
    public interface ISfTBL_Admin_UsuariosManagementServices : IGenericServices<TBL_Admin_Usuarios>
    {
        TBL_Admin_Usuarios GetUserByCredential(string trim, string s);
        List<TBL_Admin_Usuarios> FindBySpecWithRols(bool isActive);
        TBL_Admin_Usuarios GetById(int id);
    }
}
    