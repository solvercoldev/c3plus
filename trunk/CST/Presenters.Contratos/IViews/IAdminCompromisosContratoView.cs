using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminCompromisosContratoView : IView
    {
        string IdContrato { get; }

        int IdFase { get; set; }

        void LoadCompromisos(List<Compromisos> items);
        void LoadFases(List<Fases> items);
    }
}