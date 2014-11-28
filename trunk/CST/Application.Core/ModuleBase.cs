using System;
using Domain.MainModules.Entities;

namespace Application.Core
{
    public abstract class ModuleBase
    {
        public TBL_Admin_TypeByModules Seccion { get; set; }

        public void ReadSectionSettings()
        {
            if (Seccion == null)
            {
                throw new NullReferenceException("Can't access the section for settings.");
            }
        }

        public string SectionUrl { get; set; }

        public string DefaultViewControlPath
        {
            get { return Seccion==null ? string.Empty : Seccion.TBL_Admin_ModuleType.path; }
        }
    }
}