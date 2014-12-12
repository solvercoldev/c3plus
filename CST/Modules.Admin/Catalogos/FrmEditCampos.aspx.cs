using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditCampos : ViewPage<FrmEditCamposPresenter, IFrmEditCamposView>, IFrmEditCamposView 
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdCampo) ? "Nuevo Campo" : "Editar Campo");
            btnEliminar.Visible = !string.IsNullOrEmpty(IdCampo);
            btnAct.Visible = !string.IsNullOrEmpty(IdCampo);
            btnSave.Visible = string.IsNullOrEmpty(IdCampo);
            txtIdCampo.Enabled = string.IsNullOrEmpty(IdCampo);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void ListadoBloques(List<Bloques> items)
        {
            ddlBloque.DataSource = items;
            ddlBloque.DataValueField = "IdBloque";
            ddlBloque.DataTextField = "Descripcion";
            ddlBloque.DataBind();

            var li = new ListItem("--Selected--", "");
            ddlBloque.Items.Insert(0, li);
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public string Descripcion
        {
            get { return txtDescripción.Text; }
            set { txtDescripción.Text = value; }
        }

        public string IdCampo
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdCampo.Text : Request.QueryString["TemplateId"]; }
            set { txtIdCampo.Text = value; }
        }

        public string IdBloque
        {
            get { return ddlBloque.SelectedValue; }
            set { ddlBloque.SelectedValue = value; }
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
            Response.Redirect(string.Format("FrmViewCampos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewCampos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewCampos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewCampos.aspx{0}", GetBaseQueryString()));
        }

    }
}