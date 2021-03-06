﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Contratos.UI;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.UserControls
{
    public partial class WuCAdminNovedadesContrato : ViewUserControl<AdminNovedadesContratoPresenter, IAdminNovedadesContratoView>, IAdminNovedadesContratoView, IContratoWebUserControl
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
        
        protected void BtnAddNovedad_Click(object sender, EventArgs e)
        {
            var btnAccion = (Button)sender;
            TipoOperacion = btnAccion.CommandArgument;
            InitNovedad();
            ShowAdminWindow(true);
        }

        protected void BtnSaveNovedad_Click(object sender, EventArgs e)
        {
            Presenter.SaveNovedad();

            if (RiseFatherPostback != null)
                RiseFatherPostback();
        }

        #endregion

        #region DropDownList

        protected void TxtFechaInicioNovedad_TextChanged(object sender, EventArgs e)
        {
            cexTxtFechaFinNovedad.StartDate = FechaNovedad.AddDays(1);
            FechaFinNovedad = FechaNovedad.AddDays(1);

            ShowAdminWindow(true);
        }

        #endregion

        #region Repeaters

        protected void RptNovedadesList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (NovedadesContrato)(e.Item.DataItem);
                // Bindind data

                var lblTipo = e.Item.FindControl("lblTipo") as Label;
                if (lblTipo != null) lblTipo.Text = string.Format("{0}", item.TipoNovedad);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Descripcion);

                var lblResponsable = e.Item.FindControl("lblResponsable") as Label;
                if (lblResponsable != null) lblResponsable.Text = string.Format("{0}", item.TBL_Admin_Usuarios2.Nombres);

                var lblFechaInicio = e.Item.FindControl("lblFechaInicio") as Label;
                if (lblFechaInicio != null) lblFechaInicio.Text = string.Format("{0:dd/MM/yyyy}", item.FechaInicio);

                var lblFechaFin = e.Item.FindControl("lblFechaFin") as Label;
                if (lblFechaFin != null) lblFechaFin.Text = string.Format("{0:dd/MM/yyyy}", item.FechaFin);
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
            cexTxtFechaNovedad.StartDate = FechaFirma;
            

            FechaNovedad = DateTime.Now;
            cexTxtFechaFinNovedad.StartDate = DateTime.Now.AddDays(1);
            FechaFinNovedad = DateTime.Now.AddDays(1);
            Descripcion = string.Empty;

            switch (TipoOperacion)
            {
                case "Suspensión":
                    trFinNovedad.Visible = true;
                    cexTxtFechaNovedad.StartDate = FechaInicioFaseActual;
                    FechaNovedad = FechaInicioFaseActual;
                    break;
                case "Reiniciar":
                    trFinNovedad.Visible = false;
                    cexTxtFechaNovedad.StartDate = FechaInicioSuspensionContrato;
                    cexTxtFechaNovedad.EndDate = FechaFinSuspensionContrato;
                    FechaNovedad = FechaInicioSuspensionContrato.AddDays(1);
                    break;
                case "Renuncia":
                    trFinNovedad.Visible = false;
                    break;
                case "Terminación":
                    trFinNovedad.Visible = false;
                    break;
                case "Anulación":
                    trInicioNovedad.Visible = false;
                    trFinNovedad.Visible = false;
                    break;
                case "Modificación Fecha Efectiva":
                    trFinNovedad.Visible = false;
                    break;
            }
        }

        #endregion

        #region View Members

        #region Methods

        public void LoadNovedades(List<NovedadesContrato> items)
        {
            rptNovedadesList.DataSource = items;
            rptNovedadesList.DataBind();
        }

        public void ShowAdminWindow(bool visible)
        {
            if (visible)
                mpeAdminNovedad.Show();
            else
                mpeAdminNovedad.Hide();
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
        
        public string TipoOperacion
        {
            get
            {
                return string.Format("{0}", ViewState["AdminNovedadesContrato_TipoOperacion"]);
            }
            set
            {
                ViewState["AdminNovedadesContrato_TipoOperacion"] = value;
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

        public DateTime FechaNovedad
        {
            get
            {
                return Convert.ToDateTime(txtFechaNovedad.Text);
            }
            set
            {
                txtFechaNovedad.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public DateTime FechaFinNovedad
        {
            get
            {
                return Convert.ToDateTime(txtFechaFinNovedad.Text);
            }
            set
            {
                txtFechaFinNovedad.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public bool CanSuspender
        {
            get
            {
                return btnSuspender.Visible;
            }
            set
            {
                btnSuspender.Visible = value;
            }
        }

        public bool CanRestituir
        {
            get
            {
                return btnRestituir.Visible;
            }
            set
            {
                btnRestituir.Visible = value;
            }
        }

        public bool CanRenunciar
        {
            get
            {
                return btnRenunciar.Visible;
            }
            set
            {
                btnRenunciar.Visible = value;
            }
        }

        public bool CanTerminar
        {
            get
            {
                return btnTerminar.Visible;
            }
            set
            {
                btnTerminar.Visible = value;
            }
        }

        public bool CanAnular
        {
            get
            {
                return btnAnular.Visible;
            }
            set
            {
                btnAnular.Visible = value;
                btnModFechaEff.Visible = value;
            }
        }

        public DateTime FechaFirma
        {
            get
            {
                return Convert.ToDateTime(ViewState["AdminNovedadesContrato_FechaFirma"]);
            }
            set
            {
                ViewState["AdminNovedadesContrato_FechaFirma"] = value;
            }
        }

        public DateTime FechaInicioSuspensionContrato
        {
            get
            {
                return Convert.ToDateTime(ViewState["AdminNovedadesContrato_FechaInicioSuspensionContrato"]);
            }
            set
            {
                ViewState["AdminNovedadesContrato_FechaInicioSuspensionContrato"] = value;
            }
        }

        public DateTime FechaFinSuspensionContrato
        {
            get
            {
                return Convert.ToDateTime(ViewState["AdminNovedadesContrato_FechaFinSuspensionContrato"]);
            }
            set
            {
                ViewState["AdminNovedadesContrato_FechaFinSuspensionContrato"] = value;
            }
        }

        public DateTime FechaInicioFaseActual
        {
            get
            {
                return Convert.ToDateTime(ViewState["AdminNovedadesContrato_FechaInicioFaseActual"]);
            }
            set
            {
                ViewState["AdminNovedadesContrato_FechaInicioFaseActual"] = value;
            }
        }

        #endregion

        #endregion           
    }
}