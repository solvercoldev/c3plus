using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditLocalizaciones : ViewPage<FrmEditLocalizacionesPresenter, IFrmEditLocalizacionesView>, IFrmEditLocalizacionesView
    {

        public event EventHandler SaveEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdLocalizacion) ? "Nueva Localización" : "Editar Localización");
            btnAct.Visible = !string.IsNullOrEmpty(IdLocalizacion);
            btnSave.Visible = string.IsNullOrEmpty(IdLocalizacion);
            txtIdLocalizacion.Enabled = string.IsNullOrEmpty(IdLocalizacion);
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

        public string IdLocalizacion
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdLocalizacion.Text : Request.QueryString["TemplateId"]; }
            set { txtIdLocalizacion.Text = value; }
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
            Response.Redirect(string.Format("FrmViewLocalizaciones.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewLocalizaciones.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewLocalizaciones.aspx{0}", GetBaseQueryString()));
        }
    }
}