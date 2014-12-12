using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditMonedasPresenter : Presenter<IFrmEditMonedasView>
    {
        private readonly ISfMonedasManagementServices _monedas;

        public FrmEditMonedasPresenter(ISfMonedasManagementServices monedas)
        {
            _monedas = monedas;
        }
        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.ActualizarEvent += ViewActEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarMoneda();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarMoneda();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdMoneda)) return;

            var tercero = _monedas.GetById(View.IdMoneda);

            if (tercero == null) return;

            View.IdMoneda = tercero.IdMoneda;
            View.Nombre = tercero.Nombre;
        }

        private void GuardarMoneda()
        {
            try
            {
                var moneda = _monedas.NewEntity();
                moneda.IdMoneda = View.IdMoneda.ToUpper();
                moneda.Nombre = View.Nombre;

                _monedas.Add(moneda);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarMoneda()
        {
            try
            {

                if (View.IdMoneda == "") return;
                var monedas = _monedas.GetById(View.IdMoneda);
                if (monedas == null) return;

                monedas.Nombre = View.Nombre;

                _monedas.Modify(monedas);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }
    }
}