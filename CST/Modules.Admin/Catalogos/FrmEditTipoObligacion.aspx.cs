using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditTipoObligacion : ViewPage<FrmEditTipoObligacionPresenter, IFrmEditTipoObligacionView>, IFrmEditTipoObligacionView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdTipoPagoObligacion) ? "Nuevo Tipo Pago Obligación" : "Editar Tipo Pago Obligación");

            btnEliminar.Visible = !string.IsNullOrEmpty(IdTipoPagoObligacion);

            btnAct.Visible = !string.IsNullOrEmpty(IdTipoPagoObligacion);

            btnSave.Visible = string.IsNullOrEmpty(IdTipoPagoObligacion);

            txtIdTipoObligacion.Enabled = string.IsNullOrEmpty(IdTipoPagoObligacion);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmViewTipoObligacion.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public string IdTipoPagoObligacion
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdTipoObligacion.Text : Request.QueryString["TemplateId"]; }
            set { txtIdTipoObligacion.Text = value; }
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }
    }
}