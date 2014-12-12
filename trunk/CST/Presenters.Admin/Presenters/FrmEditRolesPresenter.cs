using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.Services;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditRolesPresenter : Presenter<IFrmEditRolesView>
    {

        private readonly SfTBL_Admin_RolesManagementServices _roles;
        private readonly SfTBL_Admin_UsuariosManagementServices _usuarios;

        public FrmEditRolesPresenter(SfTBL_Admin_RolesManagementServices roles,
            SfTBL_Admin_UsuariosManagementServices usuarios)
        {
            _roles = roles;
            _usuarios = usuarios;
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
            GuardarRol();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarRol();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdRol)) return;

            var rol = _roles.FindById(Convert.ToInt32(View.IdRol));

            if (rol == null) return;

            var createdBy = _usuarios.GetById(Convert.ToInt32(rol.CreateBy));
            var modifiedBy = _usuarios.GetById(Convert.ToInt32(rol.ModifiedBy));

            View.IdRol = rol.IdRol.ToString();
            View.NombreRol = rol.NombreRol;
            View.Activo = rol.Activo;
            if (rol.IsGroup != null) View.Grupo = (bool) rol.IsGroup;
            View.CreatedBy = createdBy.Nombres;
            View.CreatedOn = rol.CreateOn.ToString();
            View.ModifiedBy = modifiedBy.Nombres;
            View.ModifiedOn = rol.ModifiedOn.ToString();
        }

        private void GuardarRol()
        {
            try
            {
                var rol = _roles.NewEntity();
                rol.NombreRol = View.NombreRol;
                rol.Activo = View.Activo;
                rol.IsGroup = View.Grupo;
                rol.CreateOn = DateTime.Now;
                rol.CreateBy = View.UserSession.IdUser.ToString();
                rol.ModifiedOn = DateTime.Now;
                rol.ModifiedBy = View.UserSession.IdUser.ToString();

                _roles.Add(rol);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }

        private void ActualizarRol()
        {

            try
            {

                if (View.IdRol == "") return;
                var rol = _roles.FindById(Convert.ToInt32(View.IdRol));
                if (rol == null) return;

                rol.NombreRol = View.NombreRol;
                rol.Activo = View.Activo;
                rol.IsGroup = View.Grupo;
                rol.ModifiedOn = DateTime.Now;
                rol.ModifiedBy = View.UserSession.IdUser.ToString();

                _roles.Modify(rol);

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