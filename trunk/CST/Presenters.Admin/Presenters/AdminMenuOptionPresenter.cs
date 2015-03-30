using System;
using System.Linq;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using Domain.MainModules.Entities;

namespace Presenters.Admin.Presenters
{
    public class AdminMenuOptionPresenter : Presenter<IAdminMenuOptionView>
    {
        private readonly ISfTBL_Admin_OpcionesMenuManagementServices _optionMenuService;
        private readonly ISfTBL_Admin_RolesManagementServices _rolesService;

        public AdminMenuOptionPresenter(ISfTBL_Admin_OpcionesMenuManagementServices optionMenuService, ISfTBL_Admin_RolesManagementServices rolesService)
        {
            _optionMenuService = optionMenuService;
            _rolesService = rolesService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
            View.LoadDetalleEvent += ViewLoadDetailEvent;
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {

            if (sender == null) return;
            if (sender.Equals("Save"))
                Save();
            else
                Update();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            try
            {
                if (View.IdOpcionMenu == null) return;
                var node = _optionMenuService.FindById(Convert.ToInt32(View.IdOpcionMenu));
                if (node == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError), TypeError.Error));
                    return;
                }
                var childNode = _optionMenuService.HasChildren(node.IdOpcionMenu);
                if (childNode)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ChildNodes, node.TituloOpcion), TypeError.Error));
                    return;
                }

                _optionMenuService.Remove(node);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));

                InitializeView();
                LoadObjects();
                GetAllRoles();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void ViewLoadDetailEvent(object sender, EventArgs e)
        {
            if (sender == null) return;
            var opcion = _optionMenuService.FindById(Convert.ToInt32(sender));
            if (opcion == null)
            {
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, sender), TypeError.Error));
                return;
            }

            View.Descripcion = opcion.TituloOpcion;
            View.Posicion = string.Format("{0}", opcion.Posicion);
            View.ShowInSecondMenu = opcion.ShowSecondMenu;
            View.ShowInMainMenu = opcion.ShowMainMenu;
            View.AplicationId = opcion.AplicationId.GetValueOrDefault();
            View.Ulr = opcion.LinkUrl;
            View.Activo = opcion.Activo;
            ValidarRol(Convert.ToInt32(sender));
        }
       
        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadObjects();
            GetAllRoles();
        }

        private void Save()
        {          
            try
            {
                var newNode = new TBL_Admin_OpcionesMenu();

                var roles = View.GetSelectdRole();
                foreach (var objRol in
                    from object r in roles select _rolesService.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    newNode.TBL_Admin_Roles.Add(objRol);
                }

                newNode.TituloOpcion = View.Descripcion;
                newNode.Posicion = string.IsNullOrEmpty(View.Posicion) ? 0 : Convert.ToInt32(View.Posicion);
                newNode.ShowSecondMenu = View.ShowInSecondMenu;
                newNode.ShowMainMenu = View.ShowInMainMenu;
                newNode.IdopcionPadre = View.IdOpcionMenu ?? null;
                newNode.AplicationId = View.AplicationId;
                newNode.LinkUrl = View.Ulr;
                newNode.Activo = View.Activo;
                newNode.CreateBy = View.UserSession.UserName;
                newNode.CreateOn = DateTime.Now;
                newNode.ModifiedBy = View.UserSession.UserName;
                newNode.ModifiedOn = DateTime.Now;

                _optionMenuService.Add(newNode);

                LoadObjects();
                GetAllRoles();
                InitializeView();
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void Update()
        {
            try
            {
                if (View.IdOpcionMenu == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SelectedNode), TypeError.Error));
                    return;
                }

                var opcion = _optionMenuService.FindById(Convert.ToInt32(View.IdOpcionMenu));
                if (opcion == null) return;

                opcion.TBL_Admin_Roles.Clear();
                var roles = View.GetSelectdRole();

                foreach (var objRol in
                    from object r in roles select _rolesService.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    opcion.TBL_Admin_Roles.Add(objRol);
                }

                opcion.TituloOpcion = View.Descripcion;
                opcion.Posicion = string.IsNullOrEmpty(View.Posicion) ? 0 : Convert.ToInt32(View.Posicion);
                opcion.ShowSecondMenu = View.ShowInSecondMenu;
                opcion.ShowMainMenu = View.ShowInMainMenu;
                opcion.LinkUrl = View.Ulr;
                opcion.Activo = View.Activo;
                opcion.ModifiedBy = View.UserSession.UserName;
                opcion.ModifiedOn = DateTime.Now;

                _optionMenuService.Modify(opcion);

                LoadObjects();
                GetAllRoles();
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
                InitializeView();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #region Members

        private void LoadObjects()
        {
            var opciones = _optionMenuService.FindBySpec(true);
            View.OpcionesMenu(opciones);
        }

        private void GetAllRoles()
        {
            var listado = _rolesService.FindBySpec(true);
            View.GetAllRoles(listado);
        }

        private void ValidarRol(int nodeId)
        {
            if (nodeId == 0) return;
            var opcionMenu = _optionMenuService.FindById(nodeId);
            View.RolesAsigandos(opcionMenu.TBL_Admin_Roles);
        }

        private void InitializeView()
        {
            View.Descripcion = string.Empty;
            View.Activo = false;
            View.Posicion = string.Empty;
            View.Ulr = string.Empty;
            View.ShowInMainMenu = false;
            View.ShowInSecondMenu = false;
        }

        #endregion
    }
}