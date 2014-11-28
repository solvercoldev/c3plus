using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Contratos.UI;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.UserControls
{
    public partial class WuCAdminCompromisosFasesContrato : ViewUserControl<AdminCompromisosFasesContratoPresenter, IAdminCompromisosFasesContratoView>, IAdminCompromisosFasesContratoView, IContratoWebUserControl
    {
        #region Members

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnAddCompromiso_Click(object sender, EventArgs e)
        {
            var btnAccion = (Button)sender;
            InitNovedad();
            ShowAdminWindow(true);
        }

        protected void BtnSaveCompromiso_Click(object sender, EventArgs e)
        {
            Presenter.SaveCompromiso();
        }

        #endregion

        #region DropDownList

        #endregion

        #region Repeaters

        protected void RptCompromisosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (Compromisos)(e.Item.DataItem);
                // Bindind data

                var lblFase = e.Item.FindControl("lblFase") as Label;
                if (lblFase != null) lblFase.Text = string.Format("Fase {0}", item.Fases.NumeroFase);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Descripcion);

                var lblFechaCumplimiento = e.Item.FindControl("lblFechaCumplimiento") as Label;
                if (lblFechaCumplimiento != null) lblFechaCumplimiento.Text = string.Format("{0:dd/MM/yyyy}", item.FechaCumplimiento);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item.Estado);

                var lblResponsable = e.Item.FindControl("lblResponsable") as Label;
                if (lblResponsable != null) lblResponsable.Text = string.Format("No Campo");
            }
        }

        #endregion

        #endregion

        #region Methods

        public void LoadControlData()
        {
            Presenter.LoadInit();
        }

        void InitNovedad()
        {
            FechaCumplimiento = DateTime.Now;
            Nombre = string.Empty;
            Descripcion = string.Empty;
        }

        #endregion

        #region View Members

        #region Methods

        public void LoadCompromisos(List<Compromisos> items)
        {
            rptCompromisosList.DataSource = items;
            rptCompromisosList.DataBind();
        }

        public void LoadFases(List<Fases> items)
        {
            ddlFase.DataSource = items;
            ddlFase.DataTextField = "Nombre";
            ddlFase.DataValueField = "IdFase";
            ddlFase.DataBind();
        }

        public void LoadResponsables(List<TBL_Admin_Usuarios> items)
        {
            ddlResponsable.DataSource = items;
            ddlResponsable.DataTextField = "Nombres";
            ddlResponsable.DataValueField = "IdUser";
            ddlResponsable.DataBind();
        }

        public void ShowAdminWindow(bool visible)
        {
            if (visible)
                mpeAdminCompromiso.Show();
            else
                mpeAdminCompromiso.Hide();
        }

        #endregion

        #region Properties

        public event Action RiseFatherPostback;

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string IdContrato
        {
            get { return Request.QueryString.Get("IdContrato"); }
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text;
            }
            set
            {
                txtNombre.Text = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return txtDescripcion.Text;
            }
            set
            {
                txtDescripcion.Text = value;
            }
        }

        public DateTime FechaCumplimiento
        {
            get
            {
                return Convert.ToDateTime(txtFechaCompromiso.Text);
            }
            set
            {
                txtFechaCompromiso.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public new int IdResponsable
        {
            get
            {
                return Convert.ToInt32(ddlResponsable.SelectedValue);
            }
            set
            {
                ddlResponsable.SelectedValue = value.ToString();
            }
        }

        public int IdFase
        {
            get
            {
                return Convert.ToInt32(ddlFase.SelectedValue);
            }
            set
            {
                ddlFase.SelectedValue = value.ToString();
            }
        }

        #endregion

        #endregion
    }
}