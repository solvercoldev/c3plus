using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditUsuarios : ViewPage<FrmEditUsuarioPresenter, IFrmEditUsuariosView>, IFrmEditUsuariosView
    {
        public event EventHandler SaveEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdUser) ? "Nuevo Usuario" : "Editar Usuario");
            btnAct.Visible = !string.IsNullOrEmpty(IdUser);
            btnSave.Visible = string.IsNullOrEmpty(IdUser);
            txtIdUser.Visible = !string.IsNullOrEmpty(IdUser);
            txtIdUser.Enabled = string.IsNullOrEmpty(IdUser);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void ListadoDependencias(List<Dependencias> items)
        {
            ddlDependencia.DataSource = items;
            ddlDependencia.DataValueField = "IdDependencia";
            ddlDependencia.DataTextField = "Descripcion";
            ddlDependencia.DataBind();

            var li = new ListItem("--Selected--", "");
            ddlDependencia.Items.Insert(0, li);
        }

        public void ListadoLocalizacion(List<Localizaciones> items)
        {
            ddlLocalizacion.DataSource = items;
            ddlLocalizacion.DataValueField = "IdLocalizacion";
            ddlLocalizacion.DataTextField = "Descripcion";
            ddlLocalizacion.DataBind();

            var li = new ListItem("--Selected--", "");
            ddlLocalizacion.Items.Insert(0, li);
        }

        public void RolesAsigandos(IList<TBL_Admin_Roles> items)
        {
            foreach (RepeaterItem ri in rptRoles.Items)
            {
                var roleId = (int)ViewState[ri.UniqueID];
                var chk = (CheckBox)ri.FindControl("chkRole");
                chk.Checked = items.Any(r => r.IdRol == roleId);
            }
        }

        public ArrayList GetSelectdRole()
        {
            var arrayList = new ArrayList();
            foreach (var roleId in from RepeaterItem ri in rptRoles.Items
                                   let roleId = (int)ViewState[ri.UniqueID]
                                   let chk = (CheckBox)ri.FindControl("chkRole")
                                   where chk.Checked
                                   select roleId)
            {
                arrayList.Add(roleId);
            }
            return arrayList;
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public string IdUser
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdUser.Text : Request.QueryString["TemplateId"]; }
            set { txtIdUser.Text = value; }
        }

        public string CodigoUser
        {
            get { return txtCodigoUser.Text; }
            set { txtCodigoUser.Text = value; }
        }

        public string Nombres
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = value; }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        public string Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        public string TelefonoFijo
        {
            get { return txtTelefonoFijo.Text; }
            set { txtTelefonoFijo.Text = value; }
        }

        public string Documento
        {
            get { return txtDocumento.Text; }
            set { txtDocumento.Text = value; }
        }

        public string Movil
        {
            get { return txtMovil.Text; }
            set { txtMovil.Text = value; }
        }

        public string IdLocalizacion
        {
            get { return ddlLocalizacion.SelectedValue; }
            set { ddlLocalizacion.SelectedValue = value; }
        }

        public string Direccion
        {
            get { return txtDireccion.Text; }
            set { txtDireccion.Text = value; }
        }

        public string Extension
        {
            get { return txtExtension.Text; }
            set { txtExtension.Text = value; }
        }

        public string IdDependencia
        {
            get { return ddlDependencia.SelectedValue; }
            set { ddlDependencia.SelectedValue = value; }
        }

        public string Cargo
        {
            get { return txtCargo.Text; }
            set { txtCargo.Text = value; }
        }

        public String CreatedBy
        {
            set { LitCreatedBy.Text = value; }
        }

        public string CreatedOn
        {
            set { LitCreatedOn.Text = value; }
        }

        public String ModifiedBy
        {
            set { LiModifiedBy.Text = value; }
        }

        public string ModifiedOn
        {
            set { LiModifiedOn.Text = value; }
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmViewUsuarios.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewUsuarios.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewUsuarios.aspx{0}", GetBaseQueryString()));
        }

        public void RptRolesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var role = e.Item.DataItem as TBL_Admin_Roles;
            if (role == null) return;
            ViewState[e.Item.UniqueID] = role.IdRol;
        }

        public void GetAllRoles(IList<TBL_Admin_Roles> items)
        {
            rptRoles.DataSource = items;
            rptRoles.DataBind();
        }
    }
}