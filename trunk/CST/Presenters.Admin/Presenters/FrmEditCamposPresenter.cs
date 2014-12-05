using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditCamposPresenter : Presenter<IFrmEditCamposView>
    {
        private readonly ISfCamposManagementServices _campo;
        private readonly ISfBloquesManagementServices _bloque;

        public FrmEditCamposPresenter(ISfCamposManagementServices campo, ISfBloquesManagementServices bloque)
        {
            _campo = campo;
            _bloque = bloque;
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
            GetBloques();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarCampo();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarCampo();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarCampo();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdCampo)) return;

            var campo = _campo.GetById(View.IdCampo);

            if (campo == null) return;

            View.IdCampo = campo.IdCampo;
            View.IdBloque = campo.IdBloque;
            View.Descripcion = campo.Descripcion;
            View.Activo = campo.IsActive;
            View.CreatedBy = campo.TBL_Admin_Usuarios.Nombres;
            View.CreatedOn = campo.CreateOn.ToString();
            View.ModifiedBy = campo.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = campo.ModifiedOn.ToString();
        }

        private void GuardarCampo()
        {

            try
            {

                var campo = _campo.NewEntity();
                campo.IdCampo = View.IdCampo.ToUpper();
                campo.IdBloque = View.IdBloque;
                campo.Descripcion = View.Descripcion;
                campo.IsActive = View.Activo;
                campo.CreateOn = DateTime.Now;
                campo.CreateBy = View.UserSession.IdUser;
                campo.ModifiedOn = DateTime.Now;
                campo.ModifiedBy = View.UserSession.IdUser;

                _campo.Add(campo);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarCampo()
        {

            try
            {

                if (View.IdCampo == "") return;
                var campo = _campo.GetById(View.IdCampo);
                if (campo == null) return;

                campo.IdBloque = View.IdBloque;
                campo.Descripcion = View.Descripcion;
                campo.IsActive = View.Activo;
                campo.ModifiedOn = DateTime.Now;
                campo.ModifiedBy = View.UserSession.IdUser;

                _campo.Modify(campo);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        private void EliminarCampo()
        {
            try
            {
                if (View.IdCampo == "") return;
                var campo = _campo.GetById(View.IdCampo);
                if (campo == null) return;
                _campo.Remove(campo);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }

        private void GetBloques()
        {
            try
            {
                var listado = _bloque.FindBySpec(true);
                View.ListadoBloques(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, " Listado de Paises"), TypeError.Error));
            }
        }
    }
}