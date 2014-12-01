using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmViewCamposPresenter : Presenter<IFrmViewCamposView>
    {
        private readonly ISfCamposManagementServices _campos;

        public FrmViewCamposPresenter(ISfCamposManagementServices campos)
        {
            _campos = campos;
        }

        public override void SubscribeViewToEvents()
        {
            //View.Load += ViewLoad;
            //View.FilterEvent += ViewFilterEvent;
        }
    }
}