using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditMonedas : ViewPage<FrmEditMonedasPresenter, IFrmEditMonedasView>, IFrmEditMonedasView
    {

        public event EventHandler SaveEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdMoneda) ? "Nueva Moneda" : "Editar Moneda");
            btnAct.Visible = !string.IsNullOrEmpty(IdMoneda);
            btnSave.Visible = string.IsNullOrEmpty(IdMoneda);
            txtIdMoneda.Enabled = string.IsNullOrEmpty(IdMoneda);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string IdMoneda
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdMoneda.Text : Request.QueryString["TemplateId"]; }
            set { txtIdMoneda.Text = value; }
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmViewMonedas.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewMonedas.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewMonedas.aspx{0}", GetBaseQueryString()));
        }
    }
}