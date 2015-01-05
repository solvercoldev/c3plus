using System;
using System.Data;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.Views
{
    public partial class FrmTotalRadicados : ViewPage<TotalRadicadosPresenter, ITotalRadicadosView>, ITotalRadicadosView
    {
        #region Members

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Gestión de Radicados"));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnSelectRadicado_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var sIds = btn.CommandArgument.Split('|');

            Response.Redirect(string.Format("../Admin/FrmAdminRadicadoContrato.aspx?ModuleId={0}&IdContrato={1}&IdRadicado={2}&from=vradicados", ModuleId, sIds[0], sIds[1]));
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
                
                var lblBloque = e.Item.FindControl("lblBloque") as Label;
                if (lblBloque != null) lblBloque.Text = string.Format("{0}", item["Bloque"]);

                var lblTipoRadicado = e.Item.FindControl("lblTipoRadicado") as Label;
                if (lblTipoRadicado != null) lblTipoRadicado.Text = string.Format("{0}", item["TipoRadicado"]);

                var lblNumRadicado = e.Item.FindControl("lblNumRadicado") as Label;
                if (lblNumRadicado != null) lblNumRadicado.Text = string.Format("{0}", item["Numero"]);

                var lblFechaReciboSalida = e.Item.FindControl("lblFechaReciboSalida") as Label;
                if (lblFechaReciboSalida != null) lblFechaReciboSalida.Text = string.Format("{0:dd/MM/yyyy}", item["FechaReciboSalida"]);

                var lblAsunto = e.Item.FindControl("lblAsunto") as Label;
                if (lblAsunto != null) lblAsunto.Text = string.Format("{0}", item["Asunto"]);

                var lblResumen = e.Item.FindControl("lblResumen") as Label;
                if (lblResumen != null) lblResumen.Text = string.Format("{0}", item["Resumen"]);

                var lblEnviadoPor = e.Item.FindControl("lblEnviadoPor") as Label;
                if (lblEnviadoPor != null) lblEnviadoPor.Text = string.Format("{0}", item["UsuarioFrom"]);

                var lblEnviadoPorExterno = e.Item.FindControl("lblEnviadoPorExterno") as Label;
                if (lblEnviadoPorExterno != null) lblEnviadoPorExterno.Text = string.Format("{0}", item["FromExterno"]);

                var lblDirigidoA = e.Item.FindControl("lblDirigidoA") as Label;
                if (lblDirigidoA != null) lblDirigidoA.Text = string.Format("{0}", item["UsuarioTo"]);

                var lblDirigidoAExterno = e.Item.FindControl("lblDirigidoAExterno") as Label;
                if (lblDirigidoAExterno != null) lblDirigidoAExterno.Text = string.Format("{0}", item["ToExterno"]);

                var lblRespuestaPendiente = e.Item.FindControl("lblRespuestaPendiente") as Label;
                if (lblRespuestaPendiente != null) lblRespuestaPendiente.Text = string.Format("{0}", item["RespuestaPendiente"]);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item["Estado"]);

                var lblFechaRespuesta = e.Item.FindControl("lblFechaRespuesta") as Label;
                if (lblFechaRespuesta != null) lblFechaRespuesta.Text = string.Format("{0}", item["FechaRespuesta"]);

                var lblResponsable = e.Item.FindControl("lblResponsable") as Label;
                if (lblResponsable != null) lblResponsable.Text = string.Format("{0}", item["Responsable"]);

                var lblDependencia = e.Item.FindControl("lblDependencia") as Label;
                if (lblDependencia != null) lblDependencia.Text = string.Format("{0}", item["Dependencia"]);

                var imgSelectCompromiso = e.Item.FindControl("imgSelectRadicado") as ImageButton;
                if (imgSelectCompromiso != null) imgSelectCompromiso.CommandArgument = string.Format("{0}|{1}", item["IdContrato"], item["IdRadicado"]);
            }
        }

        #endregion

        #endregion

        #region Methods

        #endregion

        #region View Members

        #region Methods

        public void LoadRadicados(DataTable items)
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