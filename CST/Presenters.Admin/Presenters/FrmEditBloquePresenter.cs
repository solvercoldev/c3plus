using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditBloquePresenter : Presenter<IFrmEditBloqueView>
    {
        private readonly ISfBloquesManagementServices _bloque;

        public FrmEditBloquePresenter(ISfBloquesManagementServices bloque)
        {
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
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarBloque();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarBloque();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarBloque();
        }


        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdBloque)) return;

            var bloque = _bloque.GetById(View.IdBloque);

            if (bloque == null) return;

            View.IdBloque = bloque.IdBloque;
            View.Descripcion = bloque.Descripcion;
            View.Activo = bloque.IsActive;
            View.CreatedBy = bloque.TBL_Admin_Usuarios.Nombres;
            View.CreatedOn = bloque.CreateOn.ToString();
            View.ModifiedBy = bloque.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = bloque.ModifiedOn.ToString();
        }

        private void GuardarBloque()
        {

            try
            {

                var bloque = _bloque.NewEntity();
                bloque.IdBloque = View.IdBloque.ToUpper();
                bloque.Descripcion = View.Descripcion;
                bloque.IsActive = View.Activo;
                bloque.CreateOn = DateTime.Now;
                bloque.CreateBy = View.UserSession.IdUser;
                bloque.ModifiedOn = DateTime.Now;
                bloque.ModifiedBy = View.UserSession.IdUser;

                _bloque.Add(bloque);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarBloque()
        {

            try
            {

                if (View.IdBloque == "") return;
                var bloque = _bloque.GetById(View.IdBloque);
                if (bloque == null) return;

                bloque.Descripcion = View.Descripcion;
                bloque.IsActive = View.Activo;
                bloque.ModifiedOn = DateTime.Now;
                bloque.ModifiedBy = View.UserSession.IdUser;

                _bloque.Modify(bloque);

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
        private void EliminarBloque()
        {
            try
            {
                if (View.IdBloque == "") return;
                var bloque = _bloque.GetById(View.IdBloque);
                if (bloque == null) return;
                _bloque.Remove(bloque);
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