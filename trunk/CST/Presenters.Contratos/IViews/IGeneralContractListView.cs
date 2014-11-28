using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IGeneralContractListView : IView
    {
        // Filtros
        string IdBloque { get; set; }
        string Estado { get; set; }
        string DateFromStr { get; set; }
        DateTime DateFrom { get; set; }

        void LoadBloques(List<Bloques> items);

        void LoadEstados(List<DTO_ValueKey> items);

        void LoadContratos(List<Domain.MainModules.Entities.Contratos> items);
    }
}