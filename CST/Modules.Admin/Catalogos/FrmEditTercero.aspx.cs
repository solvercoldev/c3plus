using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditTercero : ViewPage<FrmEditTerceroPresenter, IFrmEditTerceroView>, IFrmEditTerceroView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdTercero) ? "Nuevo Bloque" : "Editar Bloque");

            btnEliminar.Visible = !string.IsNullOrEmpty(IdTercero);

            btnAct.Visible = !string.IsNullOrEmpty(IdTercero);

            btnSave.Visible = string.IsNullOrEmpty(IdTercero);

            txtIdTercero.Enabled = string.IsNullOrEmpty(IdTercero);
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

        public string IdTercero
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdTercero.Text : Request.QueryString["TemplateId"]; }
            set { txtIdTercero.Text = value; }
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmViewTerceros.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewTerceros.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewTerceros.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewTerceros.aspx{0}", GetBaseQueryString()));
        }

    }
}