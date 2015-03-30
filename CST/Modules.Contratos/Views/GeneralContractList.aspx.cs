using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;
using Application.Core;

namespace Modules.Contratos.Views
{
    public partial class GeneralContractList : ViewPage<GeneralContractListPresenter, IGeneralContractListView>, IGeneralContractListView
    {
        #region Members
        
        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Vista de Contratos"));
            Session["WuC_LastLoaded"] = string.Empty;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnNewContrato_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("../Admin/FrmAdminContrato.aspx?ModuleId={0}", ModuleId));
        }


        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            Presenter.LoadContratos();
        }

        #endregion

        #region DropDownList

        protected void BloquesChangeEvent(Object sender, EventArgs e)
        {
            Presenter.LoadContratos();
        }

        protected void EstadoChangeEvent(Object sender, EventArgs e)
        {
            Presenter.LoadContratos();
        }

        protected void FechaChangeEvent(object sender, EventArgs e)
        {
            var strFecha = DateFromStr;
            if (!string.IsNullOrEmpty(strFecha))
            {
                var date = new DateTime();
                strFecha = strFecha + "-01";

                if (!DateTime.TryParse(strFecha, out date))
                    return;
            }
            Presenter.LoadContratos();
        }

        #endregion

        #region Repeaters

        protected void RptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (Domain.MainModules.Entities.Contratos)(e.Item.DataItem);
                // Bindind data

                var fase = item.Fases.Where(x => DateTime.Now >= x.FechaInicio && DateTime.Now <= x.FechaFinalizacion).FirstOrDefault();

                var hplContrato = e.Item.FindControl("hplContrato") as HyperLink;
                if (hplContrato != null)
                {
                    hplContrato.Text = string.Format("{0} - {1} - Bloque: {2}", item.Nombre, item.NumeroContrato, item.Bloques.Descripcion);
                    hplContrato.NavigateUrl = string.Format("../Admin/FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, item.IdContrato);
                }

                var lblTipoContrato = e.Item.FindControl("lblTipoContrato") as Label;
                if (lblTipoContrato != null) lblTipoContrato.Text = string.Format("{0}", item.TiposContrato.Descripcion);

                var lblEmpresa = e.Item.FindControl("lblEmpresa") as Label;
                if (lblEmpresa != null) lblEmpresa.Text = string.Format("{0}", item.Empresas.RazonSocial);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item.Estado);

                var lblPeriodo = e.Item.FindControl("lblPeriodo") as Label;
                if (lblPeriodo != null) lblPeriodo.Text = string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", item.FechaInicio, item.FechaTerminacion);

                var lblFaseActual = e.Item.FindControl("lblFaseActual") as Label;
                if (lblFaseActual != null) lblFaseActual.Text = string.Format("Fase actual : {0}", fase != null ? fase.Nombre : "");
            }
        }

        #endregion

        #endregion

        #region Methods

        #endregion

        #region View Members

        #region Methods

        public void LoadBloques(List<Bloques> items)
        {
            ddlBloque.DataSource = items;
            ddlBloque.DataTextField = "Descripcion";
            ddlBloque.DataValueField = "IdBloque";
            ddlBloque.DataBind();

            ddlBloque.Items.Insert(0, new ListItem("Seleccionar Bloque", string.Empty));
        }

        public void LoadEstados(List<DTO_ValueKey> items)
        {
            ddlEstado.DataSource = items;
            ddlEstado.DataTextField = "Value";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();
        }

        public void LoadContratos(List<Domain.MainModules.Entities.Contratos> items)
        {
            rptList.DataSource = items;
            rptList.DataBind();
        }

        #endregion

        #region Properties

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }
        
        public string IdBloque
        {
            get
            {
                return ddlBloque.SelectedValue;
            }
            set
            {
                ddlBloque.SelectedValue = value;
            }
        }

        public string Estado
        {
            get
            {
                return ddlEstado.SelectedValue;
            }
            set
            {
                ddlEstado.SelectedValue = value;
            }
        }

        public DateTime DateFrom
        {
            get
            {
                var sDate = wdpFiltroDateFrom.Text.Split('-');
                return new DateTime(Convert.ToInt32(sDate[0]), Convert.ToInt32(sDate[1]), 1);
            }
            set
            {
                wdpFiltroDateFrom.Text = value.ToString("yyyy-MM");
            }
        }

        public string DateFromStr
        {
            get
            {
                return wdpFiltroDateFrom.Text;
            }
            set
            {
                wdpFiltroDateFrom.Text = value;
            }
        }

        public bool VisibleNewContract
        {
            get
            {
                return btnNuevo.Visible;
            }
            set
            {
                btnNuevo.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}