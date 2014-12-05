using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;


namespace Modules.Admin.Catalogos
{
    public partial class FrmEditPozos : ViewPage<FrmEditPozosPresenter, IFrmEditPozosView>, IFrmEditPozosView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdCampo) ? "Nuevo Pozo" : "Editar Pozo");
            btnEliminar.Visible = !string.IsNullOrEmpty(IdCampo);
            btnAct.Visible = !string.IsNullOrEmpty(IdCampo);
            btnSave.Visible = string.IsNullOrEmpty(IdCampo);
            txtIdPozo.Enabled = string.IsNullOrEmpty(IdCampo);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void ListadoCampos(List<Campos> items)
        {
            ddlCampo.DataSource = items;
            ddlCampo.DataValueField = "IdCampo";
            ddlCampo.DataTextField = "Descripcion";
            ddlCampo.DataBind();

            var li = new ListItem("--Selected--", "");
            ddlCampo.Items.Insert(0, li);
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
            get { return ddlCampo.SelectedValue; }
            set { ddlCampo.SelectedValue = value; }
            
        } 

        public string IdPozo
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdPozo.Text : Request.QueryString["TemplateId"]; }
            set { txtIdPozo.Text = value; }
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
            Response.Redirect(string.Format("FrmViewPozos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);
        }

    }
}