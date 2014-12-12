using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditLocalizacionesPresenter : Presenter<IFrmEditLocalizacionesView>
    {
        private readonly ISfLocalizacionesManagementServices _localizaciones;

        public FrmEditLocalizacionesPresenter(ISfLocalizacionesManagementServices localizaciones)
        {
            _localizaciones = localizaciones;
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
            GuardarLocalizacion();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarLocalizacion();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdLocalizacion)) return;

            var localizacion = _localizaciones.GetById(View.IdLocalizacion);

            if (localizacion == null) return;

            View.IdLocalizacion = localizacion.IdLocalizacion;
            View.Descripcion = localizacion.Descripcion;
            View.Activo = localizacion.IsActive;
            View.CreatedBy = localizacion.TBL_Admin_Usuarios.Nombres;
            View.CreatedOn = localizacion.CreateOn.ToString();
            View.ModifiedBy = localizacion.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = localizacion.ModifiedOn.ToString();
        }

        private void GuardarLocalizacion()
        {

            try
            {

                var localizacion = _localizaciones.NewEntity();
                localizacion.IdLocalizacion = View.IdLocalizacion.ToUpper();
                localizacion.Descripcion = View.Descripcion;
                localizacion.IsActive = View.Activo;
                localizacion.CreateOn = DateTime.Now;
                localizacion.CreateBy = View.UserSession.IdUser;
                localizacion.ModifiedOn = DateTime.Now;
                localizacion.ModifiedBy = View.UserSession.IdUser;

                _localizaciones.Add(localizacion);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarLocalizacion()
        {

            try
            {

                if (View.IdLocalizacion == "") return;
                var localizacion = _localizaciones.GetById(View.IdLocalizacion);
                if (localizacion == null) return;

                localizacion.Descripcion = View.Descripcion;
                localizacion.IsActive = View.Activo;
                localizacion.ModifiedOn = DateTime.Now;
                localizacion.ModifiedBy = View.UserSession.IdUser;

                _localizaciones.Modify(localizacion);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex,
                                                                           System.Reflection.MethodBase.GetCurrentMethod
                                                                               ().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }
    }
}