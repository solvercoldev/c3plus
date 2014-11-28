using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminNovedadesFasesContratoView : IView
    {
        string IdContrato { get; }

        int IdFase { get; set; }
        void LoadFases(List<Fases> items);
        
        void LoadNovedades(List<NovedadesFase> items);        
    }
}