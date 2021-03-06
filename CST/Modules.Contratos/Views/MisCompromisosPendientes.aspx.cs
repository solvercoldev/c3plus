﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.Views
{
    public partial class MisCompromisosPendientes : ViewPage<MisCompromisosPendientesPresenter, IMisCompromisosPendientesView>, IMisCompromisosPendientesView
    {
        #region Members

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Mis Compromisos Pendientes"));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnSelectCompromiso_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var sIds = btn.CommandArgument.Split('|');

            Response.Redirect(string.Format("../Admin/FrmAdminCompromisoContrato.aspx?ModuleId={0}&IdContrato={1}&IdCompromiso={2}&from=miscompendiente", ModuleId, sIds[0], sIds[1]));
        }

        #endregion

        #region UploadFiles


        #endregion

        #region Repeaters

        protected void RptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView item = (DataRowView)e.Item.DataItem;
                // Bindind data

                var lblContrato = e.Item.FindControl("lblContrato") as Label;
                if (lblContrato != null) lblContrato.Text = string.Format("{0}", item["Contrato"]);

                var lblFase = e.Item.FindControl("lblFase") as Label;
                if (lblFase != null) lblFase.Text = string.Format("{0}", item["Fase"]);

                var lblBloque = e.Item.FindControl("lblBloque") as Label;
                if (lblBloque != null) lblBloque.Text = string.Format("{0}", item["Bloque"]);

                var lblCompromiso = e.Item.FindControl("lblCompromiso") as Label;
                if (lblCompromiso != null) lblCompromiso.Text = string.Format("{0}", item["Compromiso"]);
                             
                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item["Estado"]);

                var lblFechaVencimiento = e.Item.FindControl("lblFechaVencimiento") as Label;
                if (lblFechaVencimiento != null) lblFechaVencimiento.Text = string.Format("{0:dd/MM/yyyy}", item["FechaCumplimiento"]);

                var lblTipoCompromiso = e.Item.FindControl("lblTipoCompromiso") as Label;
                if (lblTipoCompromiso != null) lblTipoCompromiso.Text = string.Format("{0}", item["TipoCompromiso"]);

                var lblImportancia = e.Item.FindControl("lblImportancia") as Label;
                if (lblImportancia != null) lblImportancia.Text = string.Format("{0}", item["Importancia"]);               

                var imgSelectCompromiso = e.Item.FindControl("imgSelectCompromiso") as ImageButton;
                if (imgSelectCompromiso != null) imgSelectCompromiso.CommandArgument = string.Format("{0}|{1}", item["IdContrato"], item["IdCompromiso"]);
            }
        }

        #endregion

        #endregion

        #region Methods

        #endregion

        #region View Members

        #region Methods

        public void LoadCompromisos(DataTable items)
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

        #endregion

        #endregion
    }
}