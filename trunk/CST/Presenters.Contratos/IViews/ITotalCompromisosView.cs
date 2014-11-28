using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Data;

namespace Presenters.Contratos.IViews
{
    public interface ITotalCompromisosView : IView
    {
        void LoadCompromisos(DataTable items);
    }
}