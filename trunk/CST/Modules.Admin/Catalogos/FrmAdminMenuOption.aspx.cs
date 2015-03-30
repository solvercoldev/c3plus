using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infragistics.WebUI.UltraWebNavigator;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmAdminMenuOption : ViewPage<AdminMenuOptionPresenter, IAdminMenuOptionView>, IAdminMenuOptionView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler LoadDetalleEvent;

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInitialize();
            if (IsPostBack) return;

        }

        private void LoadInitialize()
        {
            ImprimirTituloVentana("Administración de opciones menú principal.");
            btnDelete.Attributes.Add("onclick", "return confirm(\"¿Confirma que desea continuar?\")");
            Presenter.MessageBox += PresenterMessageBox;
            rptRoles.ItemDataBound += RptRolesItemDataBound;
        }
        #endregion

        #region Eventos

        protected void UwtOpcionesMenuNodeClicked(object sender, WebTreeNodeEventArgs e)
        {
            LimpiarMensajes();
            var nodoselected = e.Node;
            if (nodoselected == null)
            {
                ShowError("Error al seleccionar el nodo de la lista!!!");
                return;
            }
            if (nodoselected.DataKey == null)
            {
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                IdOpcionMenu = null;
                return;
            }

            IdOpcionMenu = Convert.ToInt32(nodoselected.DataKey);
            if (LoadDetalleEvent != null)
                LoadDetalleEvent(nodoselected.DataKey, EventArgs.Empty);
            ViewState["node"] = "Edit";
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            LimpiarMensajes();
            var nodoselected = uwtOpcionesMenu.SelectedNode;
            if (nodoselected == null)
            {
                ShowError("Debe seleccionar una opción de la lista!!!");
                return;
            }
            txtDescripcion.Focus();

            ViewState["node"] = "Insert";

            foreach (var chkRole in from RepeaterItem ri in rptRoles.Items select (CheckBox)ri.FindControl("chkRole"))
            {
                chkRole.Checked = false;
            }

            txtDescripcion.Text = string.Empty;
            chkActive.Checked = false;
            txtPosicion.Text = string.Empty;
            txtUrl.Text = string.Empty;
            ShowInMainMenu = false;
            ShowInSecondMenu = false;
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {

            if (ViewState["node"] == null) return;
            if (SaveEvent == null) return;

            SaveEvent(ViewState["node"].ToString() == "Insert" ? "Save" : "Update", EventArgs.Empty);

            IdOpcionMenu = null;
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
          
            IdOpcionMenu = null;
        }

        protected void RptRolesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var role = e.Item.DataItem as TBL_Admin_Roles;
                if (role == null) return;

                var chkEdit = (CheckBox)e.Item.FindControl("chkRole");                
                chkEdit.Checked = false;

                var lblNombreRol = (Label)e.Item.FindControl("lblNombreRol");
                lblNombreRol.Text = string.Format("{0}", role.NombreRol);

                var hddIdRol = (HiddenField)e.Item.FindControl("hddIdRol");
                hddIdRol.Value = string.Format("{0}", role.IdRol);
            }            
        }

        #endregion

        #region Members

        public void GetAllRoles(IEnumerable<TBL_Admin_Roles> items)
        {
            rptRoles.DataSource = items;
            rptRoles.DataBind();
        }

        public void RolesAsigandos(IEnumerable<TBL_Admin_Roles> items)
        {
            foreach (RepeaterItem ri in rptRoles.Items)
            {
                var chk = (CheckBox)ri.FindControl("chkRole");                
                var hddIdRol = (HiddenField)ri.FindControl("hddIdRol");
                var roleId = Convert.ToInt32(hddIdRol.Value);
                chk.Checked = items.Any(r => r.IdRol == roleId);
            }
        }

        public void OpcionesMenu(IEnumerable<TBL_Admin_OpcionesMenu> items)
        {
            uwtOpcionesMenu.Nodes.Clear();
            var rootNode = new Node
            {
                Text = "Nivel Principal",
                Tag = 0,
                ShowExpand = true,
                Expanded = true,
                ImageUrl = "~/Resources/images/RootNode.png"
            };

            uwtOpcionesMenu.Nodes.Add(rootNode);

            var grupos = items.Where(n => n.IdopcionPadre == null).OrderBy(x => x.Posicion);
            foreach (var node in grupos)
            {
                var tieneHijos = items.Where(i => i.IdopcionPadre == node.IdOpcionMenu).Count() > 0;
                var itemMenu = new Node
                {
                    Text = node.TituloOpcion,
                    DataKey = node.IdOpcionMenu,
                    ImageUrl = "~/Resources/images/ChildNode.png",
                    ShowExpand = tieneHijos,
                    Expanded = tieneHijos
                };
                rootNode.Nodes.Add(itemMenu);
                AddChildMenu(items, itemMenu);
            }
        }

        private static void AddChildMenu(IEnumerable<TBL_Admin_OpcionesMenu> items, Node parent)
        {
            var parentId = Convert.ToInt32(parent.DataKey);
            var childItemsMenu = items.Where(i => i.IdopcionPadre == parentId).OrderBy(x => x.Posicion);
            foreach (var child in
               childItemsMenu.Select(objItem => new Node
               {
                   Text = objItem.TituloOpcion,
                   DataKey = objItem.IdOpcionMenu,
                   ImageUrl = "~/Resources/images/ChildNode.png",
                   ShowExpand = items.Where(i => i.IdopcionPadre == objItem.IdOpcionMenu).Count() > 0,
                   Expanded = items.Where(i => i.IdopcionPadre == objItem.IdOpcionMenu).Count() > 0
               }))
            {
                parent.Nodes.Add(child);
                AddChildMenu(items, child);
            }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public string Ulr
        {
            get { return txtUrl.Text; }
            set { txtUrl.Text = value; }
        }

        public string Posicion
        {
            get { return txtPosicion.Text; }
            set { txtPosicion.Text = value; }
        }


        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public int? IdOpcionMenu
        {
            get
            {
                if (ViewState["NodoSeleccionado"] == null)
                    return null;
                return Convert.ToInt32(ViewState["NodoSeleccionado"]);
            }
            set { ViewState["NodoSeleccionado"] = value; }
        }

        public ArrayList GetSelectdRole()
        {
            var arrayList = new ArrayList();
            foreach (var roleId in from RepeaterItem ri in rptRoles.Items
                                   let roleId = (HiddenField)ri.FindControl("hddIdRol")
                                   let chk = (CheckBox)ri.FindControl("chkRole")
                                   where chk.Checked
                                   select roleId.Value)
            {
                arrayList.Add(roleId);
            }
            return arrayList;
        }

        public bool ShowInMainMenu
        {
            get
            {
                return chkShowInMainMenu.Checked;
            }
            set
            {
                chkShowInMainMenu.Checked = value;
            }
        }

        public bool ShowInSecondMenu
        {
            get
            {
                return chkShowInSecondMenu.Checked;
            }
            set
            {
                chkShowInSecondMenu.Checked = value;
            }
        }

        public int AplicationId
        {
            get { return Convert.ToInt32(txtAplicationId.Text); }
            set { txtAplicationId.Text = value.ToString(); }
        }


        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return Request.QueryString.Get("ModuleId"); }
        }

        #endregion      
    }
}
