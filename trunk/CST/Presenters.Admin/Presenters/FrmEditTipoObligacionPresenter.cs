using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditTipoObligacionPresenter : Presenter<IFrmEditTipoObligacionView>
    {
        private readonly ISfTiposPagoObligacionManagementServices _tiposPagoObligacion;

        public FrmEditTipoObligacionPresenter(ISfTiposPagoObligacionManagementServices tiposPagoObligacion)
        {
            _tiposPagoObligacion = tiposPagoObligacion;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
            View.ActualizarEvent += ViewActEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarTipoObligacion();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarTipoObligacion();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarTipoObligacion();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdTipoPagoObligacion)) return;

            var tiposPagoObligacion = _tiposPagoObligacion.GetById(View.IdTipoPagoObligacion);

            if (tiposPagoObligacion == null) return;

            View.IdTipoPagoObligacion = tiposPagoObligacion.IdTipoPagoObligacion;
            View.Descripcion = tiposPagoObligacion.Descripcion;
        }

        private void GuardarTipoObligacion()
        {

            try
            {
                var tiposPagoObligacion = _tiposPagoObligacion.NewEntity();
                tiposPagoObligacion.IdTipoPagoObligacion = View.IdTipoPagoObligacion;
                tiposPagoObligacion.Descripcion = View.Descripcion;

                _tiposPagoObligacion.Add(tiposPagoObligacion);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarTipoObligacion()
        {
            try
            {
                if (View.IdTipoPagoObligacion == "") return;
                var tiposPagoObligacion = _tiposPagoObligacion.GetById(View.IdTipoPagoObligacion);
                if (tiposPagoObligacion == null) return;

                tiposPagoObligacion.Descripcion = View.Descripcion;

                _tiposPagoObligacion.Modify(tiposPagoObligacion);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        /// <summary>
        /// Elimina la plantilla seleccionada en Base de Datos
        /// </summary>
        private void EliminarTipoObligacion()
        {
            try
            {
                if (View.IdTipoPagoObligacion == "") return;
                var tiposPagoObligacion = _tiposPagoObligacion.GetById(View.IdTipoPagoObligacion);
                if (tiposPagoObligacion == null) return;
                _tiposPagoObligacion.Remove(tiposPagoObligacion);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }
    }
}