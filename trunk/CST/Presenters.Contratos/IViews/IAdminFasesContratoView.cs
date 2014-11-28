using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using Application.MainModule.Contratos.DTO;

namespace Presenters.Contratos.IViews
{
    public interface IAdminFasesContratoView : IView
    {
        string IdContrato { get; }

        string NombreContrato { get; set; }
        string NumeroContrato { get; set; }
        string Empresa { get; set; }
        string FechaFirma { get; set; }
        string FechaEfectiva { get; set; }
        DateTime FechaFirmaInit { get; set; }
        DateTime FechaEfectivaInit { get; set; }
        string Periodo { get; set; }

        int NumeroFases { get; set; }
        bool TieneFase0 { get; set; }

        List<DTO_ValueKey> TiposFase { get; set; }
        List<DTO_ValueKey> DuracionContrato { get; set; }
        List<Dto_FaseContrato> FasesContrato { get; set; }

        void GoToAdminContrato();
        void GoToContratoList();

        int MinDuracionFaseCero { get; set; }
        int MaxDuracionFaseCero { get; set; }
        int MinDuracionFaseExploratorio { get; set; }
        int MaxDuracionFaseExploratorio { get; set; }
        int MaxTotalDuracionFaseExploratorio { get; set; }

        void AddErrorMessages(List<string> messages);
    }
}
