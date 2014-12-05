using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditTipoContrato : ViewPage<FrmEditTipoContratoPresenter, IFrmEditTipoContratoView>, IFrmEditTipoContratoView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdTipoContrato) ? "Nuevo Tipo Contrato" : "Editar Tipo Contrato");
            btnEliminar.Visible = !string.IsNullOrEmpty(IdTipoContrato);
            btnAct.Visible = !string.IsNullOrEmpty(IdTipoContrato);
            btnSave.Visible = string.IsNullOrEmpty(IdTipoContrato);
            txtIdTipoContrato.Enabled = string.IsNullOrEmpty(IdTipoContrato);
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

        public string Descripcion
        {
            get { return txtDescripción.Text; }
            set { txtDescripción.Text = value; }
        }

        public string IdTipoContrato
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdTipoContrato.Text : Request.QueryString["TemplateId"]; }
            set { txtIdTipoContrato.Text = value; }
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
            Response.Redirect(string.Format("FrmViewTipoContrato.aspx{0}", GetBaseQueryString()));
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