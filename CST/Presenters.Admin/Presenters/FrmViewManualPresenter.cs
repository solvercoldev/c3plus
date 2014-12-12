using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmViewManualPresenter : Presenter<IFrmViewManualView>
    {
        public override void SubscribeViewToEvents()
        {
            throw new NotImplementedException();
        }
    }
}