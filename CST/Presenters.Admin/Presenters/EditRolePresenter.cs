using System;
using System.Globalization;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Logging;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class EditRolePresenter : Presenter<IEditRoleView>
    {
        private readonly ISfTBL_Admin_RolesManagementServices _iRoleManagementServices;
        readonly ITraceManager _traceManager;

        public EditRolePresenter(ISfTBL_Admin_RolesManagementServices iRoleManagementServices, ITraceManager traceManager)
        {
            _iRoleManagementServices = iRoleManagementServices;
            _traceManager = traceManager;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
        }


        #region Eventos

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(View.RoleId))return;

                var role = _iRoleManagementServices.FindById(Convert.ToInt32(View.RoleId));
                _iRoleManagementServices.Remove(role);
                InitializeView();
                View.InhabiltarTodos(false);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {

                _traceManager.LogInfo(
                  string.Format(CultureInfo.InvariantCulture,
                                Message.DeleteError + " - " +   ex.Message ,
                                "User"),
                                LogType.Notify);
                InvokeMessageBox(new MessageBoxEventArgs("Error al actualizar datos.", TypeError.Error));
            }

        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            if (sender == null) return;
            if (sender.Equals("Save"))
                Save();
            else
                Update();
        }

        private void Save()
        {
            var user = _iRoleManagementServices.FindRoleByName(View.Name);
            if (user != null)
            {
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.UserExist, View.Name), TypeError.Error));
                return;
            }

            try
            {
                var newRole = _iRoleManagementServices.NewEntity();
                newRole.NombreRol = View.Name;
                newRole.Activo = View.IsActive;
                newRole.ModifiedOn = DateTime.Now;
                newRole.CreateOn = DateTime.Now;
                newRole.CreateBy =  View.UserSession.IdUser.ToString();
                newRole.ModifiedBy = View.UserSession.IdUser.ToString();
                _iRoleManagementServices.Add(newRole);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(
                  string.Format(CultureInfo.InvariantCulture,
                                Message.SaveError + " - " +  ex.Message ,
                                "User"),
                                LogType.Notify);
                InvokeMessageBox(new MessageBoxEventArgs("Error al insertar datos.", TypeError.Error));
            }
        }

        private void Update()
        {
            if(string.IsNullOrEmpty( View.RoleId))return;

            var role = _iRoleManagementServices.FindById(Convert.ToInt32(View.RoleId));
            if (role == null)
            {
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, View.Name), TypeError.Error));
                return;
            }

            try
            {

                role.NombreRol = View.Name;
                role.Activo = View.IsActive;
                role.ModifiedOn = DateTime.Now;
                role.ModifiedBy = View.UserSession.IdUser.ToString();
                _iRoleManagementServices.Modify(role);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(
                  string.Format(CultureInfo.InvariantCulture,
                                Message.SaveError + " - " + ex.Message ,
                                "User"),
                                LogType.Notify);
                InvokeMessageBox(new MessageBoxEventArgs("Error al actualizar datos.", TypeError.Error));
            }
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadObjects();
        }
       
        private void LoadObjects()
        {
            //Para el caso de que no haya cadena de consulta en la url.
            if (string.IsNullOrEmpty(View.RoleId) && !View.IsQueryString) return;

            //Para el caso de que exista Cadena de consulta pero a la hora de descifrarla arroje vacio.
            if (View.IsQueryString && string.IsNullOrEmpty(View.RoleId))
            {
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.QueryStringError), TypeError.Error));
                View.InhabiltarTodos(false);
                return;
            }

            var role = _iRoleManagementServices.FindById(Convert.ToInt32(View.RoleId));
            if (role == null)
            {
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError,"Rol"), TypeError.Error));
                View.InhabiltarTodos(false);
                return;
            }
            View.Name = role.NombreRol;
            View.IsActive = role.Activo;
        }

        private void InitializeView()
        {
            View.Name = string.Empty;
            View.IsActive = false;
        }

      
        #endregion

    }
}