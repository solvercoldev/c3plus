using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditDependenciasPresenter : Presenter<IFrmEditDependeciasView>
    {
        private readonly ISfDependenciasManagementServices _dependencias;

        public FrmEditDependenciasPresenter(ISfDependenciasManagementServices dependencias)
        {
            _dependencias = dependencias;
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
            GuardarDependencia();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarDependencia();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdDependencia)) return;

            var tiposContrato = _dependencias.GetById(View.IdDependencia);

            if (tiposContrato == null) return;

            View.IdDependencia = tiposContrato.IdDependencia;
            View.Descripcion = tiposContrato.Descripcion;
            View.Activo = tiposContrato.IsActive;
            View.CreatedBy = tiposContrato.TBL_Admin_Usuarios.Nombres;
            View.CreatedOn = tiposContrato.CreateOn.ToString();
            View.ModifiedBy = tiposContrato.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = tiposContrato.ModifiedOn.ToString();
        }

        private void GuardarDependencia()
        {

            try
            {

                var dependencias = _dependencias.NewEntity();
                dependencias.IdDependencia = View.IdDependencia.ToUpper();
                dependencias.Descripcion = View.Descripcion;
                dependencias.IsActive = View.Activo;
                dependencias.CreateOn = DateTime.Now;
                dependencias.CreateBy = View.UserSession.IdUser;
                dependencias.ModifiedOn = DateTime.Now;
                dependencias.ModifiedBy = View.UserSession.IdUser;

                _dependencias.Add(dependencias);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void ActualizarDependencia()
        {

            try
            {

                if (View.IdDependencia == "") return;
                var dependencias = _dependencias.GetById(View.IdDependencia);
                if (dependencias == null) return;

                dependencias.Descripcion = View.Descripcion;
                dependencias.IsActive = View.Activo;
                dependencias.ModifiedOn = DateTime.Now;
                dependencias.ModifiedBy = View.UserSession.IdUser;

                _dependencias.Modify(dependencias);

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