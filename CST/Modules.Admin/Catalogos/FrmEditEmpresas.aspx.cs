using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditEmpresas : ViewPage<FrmEditEmpresasPresenter, IFrmEditEmpresasView>, IFrmEditEmpresasView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(Nit) ? "Nuevo Empresa" : "Editar Empresa");
            btnEliminar.Visible = !string.IsNullOrEmpty(Nit);
            btnAct.Visible = !string.IsNullOrEmpty(Nit);
            btnSave.Visible = string.IsNullOrEmpty(Nit);
            txtNit.Enabled = string.IsNullOrEmpty(Nit);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public string Nit
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtNit.Text : Request.QueryString["TemplateId"]; }
            set { txtNit.Text = value; }
        }

        public string RazonSocial
        {
            get { return txtRazonSocial.Text; }
            set { txtRazonSocial.Text = value; }
        }

        public string Direccion
        {
            get { return txtDireccion.Text; }
            set { txtDireccion.Text = value; }
        }

        public string Telefono1
        {
            get { return txtTelefono1.Text; }
            set { txtTelefono1.Text = value; }
        }

        public string Telefono2
        {
            get { return txtTelefono2.Text; }
            set { txtTelefono2.Text = value; }
        }

        public string Logo
        {
            get { return txtLogo.Text; }
            set { txtLogo.Text = value; }
        }

        public string CreatedBy
        {
            set { LitCreatedBy.Text = value; }
        }

        public string CreatedOn
        {
            set { LitCreatedOn.Text = value; }
        }

        public string ModifiedBy
        {
            set { LiModifiedBy.Text = value; }
        }

        public string ModifiedOn
        {
            set { LiModifiedOn.Text = value; }
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmViewEmpresas.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewEmpresas.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewEmpresas.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewEmpresas.aspx{0}", GetBaseQueryString()));
        }
    }
}