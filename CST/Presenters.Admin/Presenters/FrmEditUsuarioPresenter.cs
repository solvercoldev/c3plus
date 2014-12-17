using System;
using System.Linq;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.Services;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class FrmEditUsuarioPresenter : Presenter<IFrmEditUsuariosView>
    {
        private readonly SfTBL_Admin_UsuariosManagementServices _usuario;
        private readonly SfTBL_Admin_RolesManagementServices _roles;
        private readonly ISfLocalizacionesManagementServices _localizaciones;
        private readonly ISfDependenciasManagementServices _dependencias;

        public FrmEditUsuarioPresenter(SfTBL_Admin_UsuariosManagementServices usuario, 
            SfTBL_Admin_RolesManagementServices roles, 
            ISfLocalizacionesManagementServices localizaciones,
            ISfDependenciasManagementServices dependencias)
        {
            _usuario = usuario;
            _roles = roles;
            _localizaciones = localizaciones;
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
            GetLocalizaciones();
            GetDependencias();
            GetAllRoles();
            ValidarRol();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarUsuario();
        }

        void ViewActEvent(object sender, EventArgs e)
        {
            ActualizarUsuario();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdUser)) return;

            var usuario = _usuario.GetById(Convert.ToInt32(View.IdUser));

            if (usuario == null) return;

            var createdBy = _usuario.GetById(Convert.ToInt32(usuario.CreateBy));
            var modifiedBy = _usuario.GetById(Convert.ToInt32(usuario.ModifiedBy));

            View.IdUser = usuario.IdUser.ToString();
            View.CodigoUser = usuario.CodigoUser;
            View.Nombres = usuario.Nombres;
            View.UserName = usuario.UserName;
            View.Password = usuario.Password;
            View.Email = usuario.Email;
            View.Documento = usuario.Documento;
            View.TelefonoFijo = usuario.TelefonoFijo;
            View.Movil = usuario.Movil;
            View.IdLocalizacion = usuario.IdLocalizacion;
            View.Direccion = usuario.Direccion;
            View.Extension = usuario.Extension;
            View.IdDependencia = usuario.IdDependencia;
            View.Cargo = usuario.Cargo;
            View.Activo = usuario.IsActive;
            View.CreatedBy = createdBy.Nombres;
            View.CreatedOn = usuario.CreateOn.ToString();
            View.ModifiedBy = modifiedBy.Nombres;
            View.ModifiedOn = usuario.ModifiedOn.ToString();
        }

        private void GuardarUsuario()
        {

            try
            {

                var usuario = _usuario.NewEntity();
                usuario.CodigoUser = View.CodigoUser;
                usuario.Nombres = View.Nombres;
                usuario.UserName = View.UserName;
                usuario.FechaIngreso = DateTime.Now;
                usuario.Password = View.Password;
                usuario.Email = View.Email;
                usuario.Documento = View.Documento;
                usuario.TelefonoFijo = View.TelefonoFijo;
                usuario.Movil = View.Movil;
                usuario.IdLocalizacion = View.IdLocalizacion;
                usuario.Direccion = View.Direccion;
                usuario.Extension = View.Extension;
                usuario.IdDependencia = View.IdDependencia;
                usuario.Cargo = View.Cargo;
                usuario.IsActive = View.Activo;
                usuario.CreateOn = DateTime.Now;
                usuario.CreateBy = View.UserSession.IdUser.ToString();
                usuario.ModifiedOn = DateTime.Now;
                usuario.ModifiedBy = View.UserSession.IdUser.ToString();

                var roles = View.GetSelectdRole();
                foreach (var objRol in
                from object r in roles select _roles.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    usuario.TBL_Admin_Roles.Add(objRol);
                }

                _usuario.Add(usuario);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex,System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }

        private void ActualizarUsuario()
        {

            try
            {

                if (View.IdUser == "") return;
                var usuario = _usuario.FindById(Convert.ToInt32(View.IdUser));
                if (usuario == null) return;

                usuario.CodigoUser = View.CodigoUser;
                usuario.Nombres = View.Nombres;
                usuario.UserName = View.UserName;
                usuario.Password = View.Password;
                usuario.Email = View.Email;
                usuario.Documento = View.Documento;
                usuario.TelefonoFijo = View.TelefonoFijo;
                usuario.Movil = View.Movil;
                usuario.IdLocalizacion = View.IdLocalizacion;
                usuario.Direccion = View.Direccion;
                usuario.Extension = View.Extension;
                usuario.IdDependencia = View.IdDependencia;
                usuario.Cargo = View.Cargo;
                usuario.IsActive = View.Activo;
                usuario.ModifiedOn = DateTime.Now;
                usuario.ModifiedBy = View.UserSession.IdUser.ToString();

                usuario.TBL_Admin_Roles.Clear();
                var roles = View.GetSelectdRole();

                foreach (var objRol in
                    from object r in roles select _roles.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    usuario.TBL_Admin_Roles.Add(objRol);
                }

                _usuario.Modify(usuario);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        private void GetLocalizaciones()
        {
            try
            {
                var listado = _localizaciones.FindBySpec(true);
                View.ListadoLocalizacion(listado.OrderBy(o => o.Descripcion).ToList());
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, " Listado de Localizaciones"), TypeError.Error));
            }
        }

        private void GetDependencias()
        {
            try
            {
                var listado = _dependencias.FindBySpec(true);
                View.ListadoDependencias(listado.OrderBy(o => o.Descripcion).ToList());
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, " Listado de Dependencias"), TypeError.Error));
            }
        }

        private void GetAllRoles()
        {
            var listado = _roles.FindPaged(0, 10);
            View.GetAllRoles(listado);
        }

        private void ValidarRol()
        {
            if (string.IsNullOrEmpty(View.IdUser)) return;
            var user = _usuario.FindById(Convert.ToInt32(View.IdUser));
            View.RolesAsigandos(user.TBL_Admin_Roles);
        }
    }
}