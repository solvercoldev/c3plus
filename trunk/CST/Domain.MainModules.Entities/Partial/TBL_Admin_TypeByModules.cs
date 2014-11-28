using System.Collections.Generic;

namespace Domain.MainModules.Entities
{
    public partial class TBL_Admin_TypeByModules
    {
        public string Componente
        {
            get { return TBL_Admin_ModuleType == null ? string.Empty : TBL_Admin_ModuleType.Nombre; }
        }

        public string Modulo
        {
            get { return TBL_Admin_Modulos == null ? string.Empty : TBL_Admin_Modulos.NombreModulo; }
        }

        public string PathPreView
        {
            get { return TBL_Admin_Modulos == null ? string.Empty : TBL_Admin_Modulos.PathFormPreView; }
        }
    }


    

}