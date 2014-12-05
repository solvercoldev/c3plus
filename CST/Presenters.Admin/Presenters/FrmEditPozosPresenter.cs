using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class  FrmEditPozosPresenter : Presenter<IFrmEditPozosView>
    {
        private readonly ISfPozosManagementServices _pozos;
        private readonly ISfCamposManagementServices _campos;

        public FrmEditPozosPresenter(ISfPozosManagementServices pozos, ISfCamposManagementServices campos)
        {
            _pozos = pozos;
            _campos = campos;
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
            GetCampos();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarPozo();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarPozo();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarPozo();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdPozo)) return;

            var pozo = _pozos.GetById(View.IdPozo);

            if (pozo == null) return;

            View.IdPozo = pozo.IdPozo;
            View.IdCampo = pozo.IdCampo;
            View.Descripcion = pozo.Descripcion;
            View.Activo = pozo.IsActive;
            View.CreatedBy = pozo.TBL_Admin_Usuarios.Nombres;
            View.CreatedOn = pozo.CreateOn.ToString();
            View.ModifiedBy = pozo.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = pozo.ModifiedOn.ToString();
        }

        private void GuardarPozo()
        {

            try
            {

                var pozo = _pozos.NewEntity();
                pozo.IdPozo = View.IdPozo.ToUpper();
                pozo.IdCampo = View.IdCampo;
                pozo.Descripcion = View.Descripcion;
                pozo.IsActive = View.Activo;
                pozo.CreateOn = DateTime.Now;
                pozo.CreateBy = View.UserSession.IdUser;
                pozo.ModifiedOn = DateTime.Now;
                pozo.ModifiedBy = View.UserSession.IdUser;

                _pozos.Add(pozo);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarPozo()
        {

            try
            {

                if (View.IdPozo == "") return;
                var pozo = _pozos.GetById(View.IdPozo);
                if (pozo == null) return;

                pozo.IdPozo = View.IdPozo;
                pozo.Descripcion = View.Descripcion;
                pozo.IsActive = View.Activo;
                pozo.ModifiedOn = DateTime.Now;
                pozo.ModifiedBy = View.UserSession.IdUser;

                _pozos.Modify(pozo);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        private void EliminarPozo()
        {
            try
            {
                if (View.IdPozo == "") return;
                var pozo = _pozos.GetById(View.IdPozo);
                if (pozo == null) return;
                _pozos.Remove(pozo);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }

        private void GetCampos()
        {
            try
            {
                var listado = _campos.FindBySpec(true);
                View.ListadoCampos(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, " Listado de Paises"), TypeError.Error));
            }
        }
    }
}