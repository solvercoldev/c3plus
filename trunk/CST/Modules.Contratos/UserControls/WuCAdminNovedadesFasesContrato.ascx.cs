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
    public partial class WuCAdminNovedadesFasesContrato : ViewUserControl<AdminNovedadesFasesContratoPresenter, IAdminNovedadesFasesContratoView>, IAdminNovedadesFasesContratoView, IContratoWebUserControl
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

        #endregion

        #region DropDownList

        protected void DdlFases_IndexChanged(Object sender, EventArgs e)
        {
            Presenter.LoadNovedades();
        }

        #endregion

        #region Repeaters

        protected void RptNovedadesList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (NovedadesFase)(e.Item.DataItem);
                // Bindind data

                var lblFase = e.Item.FindControl("lblFase") as Label;
                if (lblFase != null) lblFase.Text = string.Format(" {0}", item.Fases.Nombre);

                var lblTipo = e.Item.FindControl("lblTipo") as Label;
                if (lblTipo != null) lblTipo.Text = string.Format(" {0}", item.TipoNovedad);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Descripcion);

                var lblResponsable = e.Item.FindControl("lblResponsable") as Label;
                if (lblResponsable != null) lblResponsable.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var lblFechaNovedad = e.Item.FindControl("lblFechaNovedad") as Label;
                if (lblFechaNovedad != null) lblFechaNovedad.Text = string.Format("{0:dd/MM/yyyy}", item.CreateOn);
            }
        }

        #endregion

        #endregion

        #region Methods

        public void LoadControlData()
        {
            Presenter.LoadInit();
        }        

        #endregion

        #region View Members

        #region Methods

        public void LoadNovedades(List<NovedadesFase> items)
        {
            rptNovedadesList.DataSource = items;
            rptNovedadesList.DataBind();
        }

        public void LoadFases(List<Fases> items)
        {
            ddlFases.DataSource = items;
            ddlFases.DataTextField = "Nombre";
            ddlFases.DataValueField = "IdFase";
            ddlFases.DataBind();

            ddlFases.Items.Insert(0, new ListItem("Ver todas las fases", "0"));
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

        public int IdFase
        {
            get
            {
                return Convert.ToInt32(ddlFases.SelectedValue);
            }
            set
            {
                ddlFases.SelectedValue = value.ToString();
            }
        }
     
        #endregion

        #endregion        
    }
}