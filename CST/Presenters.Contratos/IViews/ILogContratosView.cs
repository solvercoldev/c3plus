using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface ILogContratosView : IView
    {
        #region Events

        event EventHandler FilterEvent;

        #endregion

        #region Members

        void LogsList(List<LogContratos> items);

        string IdContrato { get; }

        bool IsLoadedControl { get; set; }

        #endregion

    }
}