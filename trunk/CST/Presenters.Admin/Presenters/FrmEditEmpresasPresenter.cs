using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditEmpresasPresenter : Presenter<IFrmEditEmpresasView>
    {
        private readonly ISfEmpresasManagementServices _empresas;
        
        public FrmEditEmpresasPresenter(ISfEmpresasManagementServices empresa)
        {
            _empresas = empresa;
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
            GuardarEmpresa();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarEmpresa();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarEmpresa();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.Nit)) return;

            var empresa = _empresas.GetById(View.Nit);

            if (empresa == null) return;

            View.Nit = empresa.Nit;
            View.RazonSocial = empresa.RazonSocial;
            View.Direccion = empresa.Direccion;
            View.Telefono1 = empresa.TelefonoUno;
            View.Telefono2 = empresa.TelefonoDos;
            View.Logo = empresa.Logo;
            View.Activo = empresa.IsActive;
            //View.CreatedBy = bloque.CreateBy.ToString();
            //View.CreatedOn = bloque.CreateOn.ToString();
            //View.ModifiedBy = bloque.ModifiedBy.ToString();
            //View.ModifiedOn = bloque.ModifiedOn.ToString();
        }

        private void GuardarEmpresa()
        {

            try
            {

                var empresa = _empresas.NewEntity();
                empresa.Nit = View.Nit;
                empresa.RazonSocial = View.RazonSocial;
                empresa.Direccion = View.Direccion;
                empresa.TelefonoUno = View.Telefono1;
                empresa.TelefonoDos = View.Telefono2;
                empresa.Logo = View.Logo;
                empresa.IsActive = View.Activo;
                empresa.CreateOn = DateTime.Now;
                empresa.CreateBy = View.UserSession.IdUser;
                empresa.ModifiedOn = DateTime.Now;
                empresa.ModifiedBy = View.UserSession.IdUser;

                _empresas.Add(empresa);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex,
                                                                           System.Reflection.MethodBase.GetCurrentMethod
                                                                               ().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }

        private void ActualizarEmpresa()
        {

            try
            {

                if (View.Nit == "") return;
                var empresa = _empresas.GetById(View.Nit);
                if (empresa == null) return;

                empresa.RazonSocial = View.RazonSocial;
                empresa.Direccion = View.Direccion;
                empresa.TelefonoUno = View.Telefono1;
                empresa.TelefonoDos = View.Telefono2;
                empresa.Logo = View.Logo;
                empresa.IsActive = View.Activo;
                empresa.ModifiedOn = DateTime.Now;
                empresa.ModifiedBy = View.UserSession.IdUser;

                _empresas.Modify(empresa);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        private void EliminarEmpresa()
        {
            try
            {
                if (View.Nit == "") return;
                var empresa = _empresas.GetById(View.Nit);
                if (empresa == null) return;
                _empresas.Remove(empresa);
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