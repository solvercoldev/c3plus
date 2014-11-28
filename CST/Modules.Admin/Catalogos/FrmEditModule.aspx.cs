using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infragistics.Web.UI.EditorControls;
using Infragistics.Web.UI.NavigationControls;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using System.Web.UI;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditModule : ViewPage<EditModulePresenter, IEditModulesView>, IEditModulesView
    {
        public event EventHandler SelectedModuleEvent;
        public event EventHandler EditSectionEvent;
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ModulesEvent;
        public event EventHandler ComponentsEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RegistrarScript();
            rptSections.ItemDataBound += rptSections_ItemDataBound;
            rptSections.ItemCommand += RptSectionsItemCommand;
        }

       

        #region Eventos

        void RptSectionsItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;
            if(EditSectionEvent!=null)
            {
                dvRutas.ChangeMode(DetailsViewMode.Edit);
                IdSeccion = e.CommandArgument.ToString();
                EditSectionEvent(e.CommandArgument, EventArgs.Empty);
            }
        }

        void rptSections_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var oSeccion = e.Item.DataItem as TBL_Admin_TypeByModules;
            if (oSeccion == null) return;

            var lnkEdit = e.Item.FindControl("lnkEdit") as LinkButton;
            if (lnkEdit != null)
            {
                lnkEdit.CommandArgument = oSeccion.OID.ToString();
            }
        }

        protected void ContextMenuClick(object sender, DataMenuItemEventArgs e)
        {
            switch (e.Item.Key)
            {

                case "Select":
                    if (SelectedModuleEvent != null)
                        SelectedModuleEvent(hndNodeSelected.Value, EventArgs.Empty);
                    
                    dvRutas.ChangeMode(DetailsViewMode.ReadOnly);
                    EditSectionEvent(string.Empty, EventArgs.Empty);
                   
                    break;
            }

        }

        protected void OnDataBoundEvent(object sender, EventArgs e)
        {
            var ddlIdModulo = dvRutas.FindControl("ddlIdModulo") as DropDownList;
            if (ddlIdModulo != null)
            {
                if (ModulesEvent != null)
                    ModulesEvent(null, EventArgs.Empty);

                var hdnIdModulo = dvRutas.FindControl("hdnIdModulo") as HtmlInputHidden;
                if (hdnIdModulo != null)
                    ddlIdModulo.SelectedValue = hdnIdModulo.Value;
            }

            var ddlComponente = dvRutas.FindControl("ddlComponente") as DropDownList;
            if (ddlComponente != null)
            {
                if (ComponentsEvent != null)
                    ComponentsEvent(null, EventArgs.Empty);

                var hdnIdComponente = dvRutas.FindControl("hdnIdComponente") as HtmlInputHidden;
                if (hdnIdComponente != null)
                    ddlComponente.SelectedValue = hdnIdComponente.Value;
            }
        }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IdSeccion))
            {
                if (dvRutas.CurrentMode == DetailsViewMode.ReadOnly)
                {
                    ActualizarDetailsView(DetailsViewMode.Edit);
                    btnEliminar.Attributes.Remove("onclick");
                }
                else
                {
                    Guardar();
                    ActualizarDetailsView(DetailsViewMode.ReadOnly);
                }
            }
            else
            {
                Guardar();
                ActualizarDetailsView(DetailsViewMode.ReadOnly);
            }
        }

        protected void BtnEliminarClick(object sender, EventArgs e)
        {
            if (btnEliminar.Text == @"Cancelar")
            {

                ActualizarDetailsView(DetailsViewMode.ReadOnly);
                if (!string.IsNullOrEmpty(hndNodeSelected.Value))
                    btnEliminar.Attributes.Add("onclick", "return confirm('¿Confirma que desea eliminar el registro seleccionado?');");
            }
            else
            {
                EliminarEvent();
                ActualizarDetailsView(DetailsViewMode.ReadOnly);
                btnEliminar.Attributes.Remove("onclick");
            }
        }


        protected void DllModuloSelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = (DropDownList) sender;
            if(ddl == null)return;
            var txtPathPreView = ((TextBox)dvRutas.FindControl("txtPathPreView"));
            if (ViewState[ddl.SelectedValue]!=null)
            {
                if (txtPathPreView != null)
                {
                    txtPathPreView.Text = ViewState[ddl.SelectedValue].ToString();
                    //LoadPlaceHolder();
                }
            }
            else
            {
                if (txtPathPreView != null)
                    txtPathPreView.Text = string.Empty;
            }
        }

        #endregion

        private void EliminarEvent()
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }

        private void Guardar()
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }

        public string IdSeccion
        {
            get { return ViewState["IdSeccion"] == null ? string.Empty : ViewState["IdSeccion"].ToString(); }
            set { ViewState["IdSeccion"] = value; }
        }

       
        private void ConfigurarControles()
        {
            if (dvRutas.CurrentMode == DetailsViewMode.Edit)
            {
                btnGuardar.Text = @"Guardar";
                btnEliminar.Text = @"Cancelar";
                btnGuardar.Visible = true;
                btnEliminar.Visible = true;
            }
            else if (dvRutas.CurrentMode == DetailsViewMode.ReadOnly)
            {
                if (!string.IsNullOrEmpty(hndNodeSelected.Value))
                {
                    btnEliminar.Text = @"Eliminar";
                    btnGuardar.Text = @"Editar";
                    btnGuardar.Visible = true;
                    btnEliminar.Visible = true;
                }
                else
                {
                    btnGuardar.Visible = false;
                    btnEliminar.Visible = false;
                }
            }
            else
            {
                btnGuardar.Text = @"Guardar";
                btnEliminar.Text = @"Cancelar";
                btnGuardar.Visible = true;
                btnEliminar.Visible = true;
            }
        }

       
        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get {  return ModuleId; }
        }
        
        public void ListadoModulos(List<TBL_Admin_Modulos> items)
        {
           
            wdtModulos.Nodes.Clear();

            var rootNode = new DataTreeNode
            {
                Text = "Modulos del Sistema",
                Key = string.Empty,
                Value = string.Empty,
                Expanded = true,
                ImageUrl = "~/Resources/Images/Rutas.png"
            };

            wdtModulos.Nodes.Add(rootNode);

            foreach (var n in items.Select(opcion => new DataTreeNode
            {
                Text = string.Format("{0}", opcion.NombreModulo),
                Key = opcion.IdModulo.ToString(),
                ImageUrl = "~/Resources/Images/ChildNode32.png",
                Value = opcion.IdModulo.ToString(),
                IsEmptyParent = false
            }))
            {
                rootNode.Nodes.Add(n);
            }
        }

        public void ListadoSecciones(List<TBL_Admin_TypeByModules> items)
        {
            rptSections.DataSource = items;
            rptSections.DataBind();
        }

        public void CargarSeccionById(List<TBL_Admin_TypeByModules> items)
        {

            if (items.Count > 0)
                ConfigurarControles();
            else
            {
                dvRutas.ChangeMode(DetailsViewMode.Insert);
                btnGuardar.Visible = false;
                btnEliminar.Visible = false;
            }

            dvRutas.DataSource = items;
            dvRutas.DataBind();

           
            
        }

        public void ListadoModulosSeccion(List<TBL_Admin_Modulos> items)
        {
            var ddlIdModulo = ((DropDownList)dvRutas.FindControl("ddlIdModulo"));
            ddlIdModulo.Items.Clear();
            foreach (var item in items)
            {
                var li = new ListItem(item.NombreModulo, item.IdModulo.ToString());
                ddlIdModulo.Items.Add(li);
                if (!string.IsNullOrEmpty(item.PathFormPreView))
                ViewState[string.Format("{0}", item.IdModulo)] = item.PathFormPreView;
            }

        }

        public void ListadoComponentesSeccion(List<TBL_Admin_ModuleType> items)
        {
            var ddlComponente = ((DropDownList)dvRutas.FindControl("ddlComponente"));
            ddlComponente.Items.Clear();
            foreach (var item in items)
            {
                var li = new ListItem(item.Nombre, item.IdModuleType.ToString());
                ddlComponente.Items.Add(li);
            }
        }

        public bool MostrarTitulo
        {
            get { return ((CheckBox)dvRutas.FindControl("chkValidaMostrarTitulo")).Checked; }
            set { ((CheckBox)dvRutas.FindControl("chkValidaMostrarTitulo")).Checked = value; }
        }

        public string PlaceHolder
        {
            get { return ((TextBox)dvRutas.FindControl("txtPlaceHolder")).Text; }
            set { ((TextBox)dvRutas.FindControl("txtPlaceHolder")).Text = value; }
        }

        public int Position
        {
            get { return ((WebNumericEditor)dvRutas.FindControl("txtPosicion")).ValueInt; }
            set { ((WebNumericEditor)dvRutas.FindControl("txtPosicion")).ValueInt = value; }
        }

        public string Titulo
        {
            get { return ((TextBox)dvRutas.FindControl("txtTitulo")).Text; }
            set { ((TextBox)dvRutas.FindControl("txtTitulo")).Text = value; }
        }

        public string PathPreView
        {
            get { return ((TextBox)dvRutas.FindControl("txtPathPreView")).Text; }
            set { ((TextBox)dvRutas.FindControl("txtPathPreView")).Text = value; }
        }

        public bool Activar
        {
            get { return ((CheckBox)dvRutas.FindControl("chkActivarEdit")).Checked; }
            set { ((CheckBox)dvRutas.FindControl("chkActivarEdit")).Checked = value; }
        }


        public int IdComponente
        {

            get
            {
                return string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlComponente")).SelectedValue)
                           ? 0
                           : Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlComponente")).SelectedValue);
            }
            set { ((DropDownList)dvRutas.FindControl("ddlComponente")).SelectedValue = value.ToString(); }
        }

        public int? IdModulo
        {
            get
            {
                return string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlIdModulo")).SelectedValue)
                           ? 0
                           : Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlIdModulo")).SelectedValue);
            }
            set { ((DropDownList)dvRutas.FindControl("ddlIdModulo")).SelectedValue = value.ToString(); }
        }

        private void ActualizarDetailsView(DetailsViewMode mode)
        {
            dvRutas.ChangeMode(mode);
            if (EditSectionEvent!=null)
                EditSectionEvent(IdSeccion, EventArgs.Empty);

            if (SelectedModuleEvent != null)
                SelectedModuleEvent(hndNodeSelected.Value, EventArgs.Empty);
        }

        private void RegistrarScript()
        {
            var strScript = " var lastNode = null; " +
                            " function MenuItem_Click(menu, eventArgs) { " +
                            " switch (eventArgs.getItem().get_key()) { " +
                            " case 'Edit': " +
                            "   if (lastNode != null  ) { " +
                            "     if(lastNode.get_key() == '')" +
                            "       eventArgs.set_cancel(true); " +
                            "   } " +
                            "   break; " +
                            " case 'expand': " +
                            "   if (lastNode != null) {" +
                            "       lastNode.toggle(true, true); " +
                            "       eventArgs.set_cancel(true); " +
                            "  } break; " +
                            "   } " +
                            "}" +

                            " function Node_Click(tree, eventArgs) { " +
                            " lastNode = eventArgs.getNode(); " +
                            " var menu = $find('" + ContextMenu.ClientID + "'); " +
                            " var oHdnSelected = document.getElementById('" + hndNodeSelected.ClientID + "'); " +
                            " if (menu != null && eventArgs.get_browserEvent() != null && eventArgs.get_browserEvent().button == 2) {" +
                            "       oHdnSelected.value = eventArgs.getNode().get_key(); " +
                            "       menu.showAt(null, null, eventArgs.get_browserEvent()); " +
                            "    } " +
                            " }";

            var sm = ScriptManager.GetCurrent(Page);
            if (null != sm && sm.IsInAsyncPostBack)
                ScriptManager.RegisterStartupScript(this, GetType(), "customControlScript", strScript, true);
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "customControlScript", strScript, true);
        }


        #region Codigo Prueba

        //private void LoadPlaceHolder()
        //{
        //    var writer = new StringWriter();

        //    Server.Execute("~/Pages/Modules/Pedidos/Catalogos/FrmPreViewPedido.aspx", writer);

        //    string str_out = writer.GetStringBuilder().ToString();

        //    var subjectRegex = new Regex(@"\<asp:PlaceHolder\>\r\n(.*)\r\n\<\/asp:PlaceHolder\>"
        //      , RegexOptions.Compiled | RegexOptions.Singleline);

        //    var subject = subjectRegex.Match(str_out).Groups[1].Value;

        //}
        #endregion

    }
}