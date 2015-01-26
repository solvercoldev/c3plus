using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IManualesANHView : IView
    {
        void LoadManuales(List<ManualAnh> items);
    }
}