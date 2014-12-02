using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.MainModule.Contratos.DTO;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Contratos.UI;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;
using Application.Core;

namespace Modules.Contratos.Admin
{
    public partial class FrmAdminComentarioRespuesta : ViewPage<AdminComentarioRespuestaPresenter, IAdminComentarioRespuestaView>, IAdminComentarioRespuestaView
    {
        #region Members

        public string IdContratoQS
        {
            get { return Request.QueryString.Get("IdContrato"); }
        }

        string PathAttachedFiles
        {
            get
            {
                if (ViewState["PathFiles"] == null)
                    ViewState["PathFiles"] = ConfigurationManager.AppSettings.Get("UploadFilesFolder");

                return ViewState["PathFiles"].ToString();
            }

            set { ViewState["PathFiles"] = value; }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Comentarios y Respuestas -  {0}", NombreContrato));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Menu

        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
        }
        
        protected void BtnEditComentarioClick(object sender, EventArgs e)
        {
            EnableEdit(true);
            Presenter.LoadArhchivosAdjuntos();
        }

        protected void BtnCancelComentarioClick(object sender, EventArgs e)
        {
            EnableEdit(false);
            Presenter.LoadArhchivosAdjuntos();
        }

        protected void BtnSaveComentarioClick(object sender, EventArgs e)
        {
            Presenter.AddComentarioRelacionado();
        }

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivo.HasFile)
            {
                return;
            }

            Presenter.AddArchivoAdjunto();
        }

        protected void BtnRemoveArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var IdArchivo = Convert.ToDecimal(btn.CommandArgument);

            Presenter.RemoveArchivoAdjunto(IdArchivo);
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = Convert.ToDecimal(btn.CommandArgument);

            Presenter.DownloadArchivoAdjunto(IdArchivo);
        }

        #endregion

        #region DropDownList

        #endregion

        #region Repeaters

        protected void RptComentariosAsociados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (ComentariosRespuesta)(e.Item.DataItem);
                // Bindind data

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblCreadoPor = e.Item.FindControl("lblCreadoPor") as Label;
                if (lblCreadoPor != null) lblCreadoPor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var lblComentario = e.Item.FindControl("lblComentario") as Label;
                if (lblComentario != null) lblComentario.Text = string.Format("{0}", item.Comentario);
            }
        }

        protected void RptArchivosAdjuntos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (DTO_ValueKey)(e.Item.DataItem);
                // Bindind data

                var hddIdArchivo = e.Item.FindControl("hddIdArchivo") as HiddenField;
                if (hddIdArchivo != null) hddIdArchivo.Value = string.Format("{0}", item.Id);

                var lnkNombreArchivo = e.Item.FindControl("lnkNombreArchivo") as LinkButton;
                if (lnkNombreArchivo != null)
                {
                    lnkNombreArchivo.Text = string.Format("{0}", item.Value);
                    lnkNombreArchivo.CommandArgument = string.Format("{0}", item.Id);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(lnkNombreArchivo);
                }

                var imgDeleteAnexo = e.Item.FindControl("imgDeleteAnexo") as ImageButton;
                if (imgDeleteAnexo != null)
                {
                    imgDeleteAnexo.CommandArgument = string.Format("{0}", item.Id);
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        #endregion

        #region View Members

        #region Methods

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();

            trAnexos.Visible = items.Any();
        }

        public void DescargarArchivo(DTO_ValueKey archivo)
        {
            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");
        }

        public void LoadUsuariosCopia(List<Application.Core.DTO_ValueKey> items)
        {
            lstUsuariosCopia.DataSource = items;
            lstUsuariosCopia.DataValueField = "Id";
            lstUsuariosCopia.DataTextField = "Value";
            lstUsuariosCopia.DataBind();

            trUsuariosCopia.Visible = items.Any();
        }

        public void EnableEdit(bool enabled)
        {
            trComentarios.Visible = enabled;
            trDestinatarios.Visible = enabled;
            txtComentario.Text = string.Empty;

            fupAnexoArchivo.Visible = enabled;
            btnAddArchivoAdjunto.Visible = enabled;

            btnCancel.Visible = enabled;
            btnSave.Visible = enabled;
            btnEdit.Visible = !enabled;
        }

        public void LoadDestinatarios(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddDestinatarios.DataSource = items;
            wddDestinatarios.DataTextField = "Nombres";
            wddDestinatarios.DataValueField = "IdUser";
            wddDestinatarios.DataBind();
        }

        public void LoadComentariosRelacionados(List<ComentariosRespuesta> items)
        {
            rptComentariosAsociados.DataSource = items;
            rptComentariosAsociados.DataBind();

            trComentariosRespuesta.Visible = items.Any();
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

        public string IdContrato
        {
            get
            {
                return ViewState["AdminFasesContrato_IdContrato"] == null ? IdContratoQS : ViewState["AdminFasesContrato_IdContrato"].ToString();
            }
            set
            {
                ViewState["AdminFasesContrato_IdContrato"] = value;
            }
        }

        public string Empresa
        {
            get
            {
                return lblEmpresa.Text;
            }
            set
            {
                lblEmpresa.Text = value;
            }
        }

        public string Bloque
        {
            get
            {
                return lblBloque.Text;
            }
            set
            {
                lblBloque.Text = value;
            }
        }

        public string FechaFirma
        {
            get
            {
                return lblFechaFirma.Text;
            }
            set
            {
                lblFechaFirma.Text = UppercaseFirst(value);
            }
        }

        public string Periodo
        {
            get
            {
                return lblPeriodo.Text;
            }
            set
            {
                lblPeriodo.Text = UppercaseFirst(value);
            }
        }

        public string NombreContrato
        {
            get
            {
                return ViewState["AdminFasesContrato_NombreContrato"] == null ? "Administrar Fases Contrato" : ViewState["AdminFasesContrato_NombreContrato"].ToString();
            }
            set
            {
                ViewState["AdminFasesContrato_NombreContrato"] = value;
            }
        }

        public string NumeroContrato
        {
            get
            {
                return lblContrato.Text;
            }
            set
            {
                lblContrato.Text = value;
            }
        }

        public string IdComentario
        {
            get { return Request.QueryString["IdComentario"]; }
        }

        public string Asunto
        {
            get
            {
                return lblAsunto.Text;
            }
            set
            {
                lblAsunto.Text = value;
            }
        }

        public string Mensaje
        {
            get
            {
                return lblMensaje.Text;
            }
            set
            {
                lblMensaje.Text = value;
            }
        }

        public string Destinatario
        {
            get
            {
                return lblDestinatario.Text;
            }
            set
            {
                lblDestinatario.Text = value;
            }
        }

        public DateTime FechaComentario
        {
            get
            {
                return Convert.ToDateTime(lblFecha.Text);
            }
            set
            {
                lblFecha.Text = string.Format("{0:dd/MM/yyyy}", value);
            }
        }

        public string NuevoComentario
        {
            get
            {
                return txtComentario.Text;
            }
            set
            {
                txtComentario.Text = value;
            }
        }

        public string IdUsuarioDestino
        {
            get
            {
                return wddDestinatarios.SelectedValue;
            }
            set
            {
                wddDestinatarios.SelectedValue = value;
            }
        }

        public byte[] ArchivoAdjunto
        {
            get { return fupAnexoArchivo.FileBytes; }
        }

        public string NombreArchivoAdjunto
        {
            get { return fupAnexoArchivo.FileName; }
        }

        public string TipoContrato
        {
            get
            {
                return lblTipoContrato.Text;
            }
            set
            {
                lblTipoContrato.Text = value;
            }
        }

        public string FechaEfectiva
        {
            get
            {
                return lblFechaEfectiva.Text;
            }
            set
            {
                lblFechaEfectiva.Text = UppercaseFirst(value);
            }
        }

        #endregion

        #endregion           
    }
}