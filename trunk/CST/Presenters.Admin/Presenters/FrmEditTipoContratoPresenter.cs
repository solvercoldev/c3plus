using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditTipoContratoPresenter : Presenter<IFrmEditTipoContratoView>
    {
        private readonly ISfTiposContratoManagementServices _tipoContrato;

        public FrmEditTipoContratoPresenter(ISfTiposContratoManagementServices tipoContrato)
        {
            _tipoContrato = tipoContrato;
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
            GuardarTipoContrato();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarTipoContrato();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarTipoContrato();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdTipoContrato)) return;

            var tiposContrato = _tipoContrato.GetById(View.IdTipoContrato);

            if (tiposContrato == null) return;

            View.IdTipoContrato = tiposContrato.IdTipoContrato;
            View.Descripcion = tiposContrato.Descripcion;
            View.Activo = tiposContrato.IsActive;
            View.CreatedBy = tiposContrato.TBL_Admin_Usuarios.Nombres;
            View.CreatedOn = tiposContrato.CreateOn.ToString();
            View.ModifiedBy = tiposContrato.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = tiposContrato.ModifiedOn.ToString();
        }

        private void GuardarTipoContrato()
        {

            try
            {

                var tipoContato = _tipoContrato.NewEntity();
                tipoContato.IdTipoContrato = View.IdTipoContrato.ToUpper();
                tipoContato.Descripcion = View.Descripcion;
                tipoContato.IsActive = View.Activo;
                tipoContato.CreateOn = DateTime.Now;
                tipoContato.CreateBy = View.UserSession.IdUser;
                tipoContato.ModifiedOn = DateTime.Now;
                tipoContato.ModifiedBy = View.UserSession.IdUser;

                _tipoContrato.Add(tipoContato);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarTipoContrato()
        {

            try
            {

                if (View.IdTipoContrato == "") return;
                var tiposContrato = _tipoContrato.GetById(View.IdTipoContrato);
                if (tiposContrato == null) return;

                tiposContrato.Descripcion = View.Descripcion;
                tiposContrato.IsActive = View.Activo;
                tiposContrato.ModifiedOn = DateTime.Now;
                tiposContrato.ModifiedBy = View.UserSession.IdUser;

                _tipoContrato.Modify(tiposContrato);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        private void EliminarTipoContrato()
        {
            try
            {
                if (View.IdTipoContrato == "") return;
                var bloque = _tipoContrato.GetById(View.IdTipoContrato);
                if (bloque == null) return;
                _tipoContrato.Remove(bloque);
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