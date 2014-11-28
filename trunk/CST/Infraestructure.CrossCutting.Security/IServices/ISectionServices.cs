using Domain.MainModules.Entities;

namespace Infraestructure.CrossCutting.Security.IServices
{
    public interface ISectionServices
    {
        //TBL_Admin_TypeByModules GetModuletype(string idModule);

        TBL_Admin_Modulos GetModuleById(string idModule);
    }
}