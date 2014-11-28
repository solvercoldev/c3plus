using System;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infraestructure.CrossCutting.Security.IServices;

namespace Infraestructure.CrossCutting.Security.Services
{
    public class SectionServices : ISectionServices
    {
        //private readonly ISfTBL_Admin_TypeByModulesManagementServices _sectionServices;
        private readonly ISfTBL_Admin_ModulosManagementServices _modulesServices;

        public SectionServices(
            //ISfTBL_Admin_TypeByModulesManagementServices sectionServices, 
            ISfTBL_Admin_ModulosManagementServices modulesServices)
        {
            //_sectionServices = sectionServices;
            _modulesServices = modulesServices;
        }

        //public TBL_Admin_TypeByModules GetModuletype(string idModule)
        //{
        //    return  _sectionServices.GetSectionByIdModule(idModule);
        //}

        public TBL_Admin_Modulos GetModuleById(string idModule)
        {
            return _modulesServices.GetModuleById(Convert.ToInt32(idModule));
        }
    }
}