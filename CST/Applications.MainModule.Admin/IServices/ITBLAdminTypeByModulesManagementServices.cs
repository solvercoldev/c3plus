//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por una plantilla T4 de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  Abril 24 de 2012.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System.Collections.Generic;
using Domain.Core;
using Domain.MainModules.Entities;

namespace Applications.MainModule.Admin.IServices
{
    public interface ISfTBL_Admin_TypeByModulesManagementServices : IGenericServices<TBL_Admin_TypeByModules>
    {
        TBL_Admin_TypeByModules GetSectionByIdModule(string idModule);

        List<TBL_Admin_TypeByModules> ListadoSeccionesPormodulo(int idModule);

        List<TBL_Admin_TypeByModules> SeccionporId(int idSecion);
    }
}
    