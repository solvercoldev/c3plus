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
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Applications.MainModule.Admin.IServices
{
    public interface ISfTBL_Admin_SeccionesManagementServices : IGenericServices<TBL_Admin_Secciones>
    {
        /// <summary>
        /// Obtine el listado de secciones filtradas por el modulo
        /// </summary>
        /// <param name="idModulo"></param>
        /// <returns></returns>
        IEnumerable<TBL_Admin_Secciones> ListadoSeccionesPorModulo(ModulosAplicacion idModulo);

        /// <summary>
        /// Obtine el listado de secciones filtradas por el modulo
        /// </summary>
        /// <param name="idModulo"></param>
        /// <returns></returns>
        IEnumerable<TBL_Admin_Secciones> ListadoSeccionesPorModulo(int idModulo);
    }
}
    