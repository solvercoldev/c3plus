using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Contratos.UI;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.UserControls
{
    public partial class WuCAdminComentariosContrato : ViewUserControl<AdminComentariosContratoPresenter, IAdminComentariosContratoView>, IAdminComentariosContratoView, IContratoWebUserControl
    {
        #region Members        

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddComentario_Click(object sender, EventArgs e)
        {
            InitAdminComentario();
            trInfoDestinatario.Visible = true;
            ShowAdminComentarioWindow(true);
        }

        protected void BtnSaveComentario_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Asunto))
                messages.Add("Es necesario ingresar un asunto.");

            if (string.IsNullOrEmpty(Comentario))
                messages.Add("Es necesario ingresar un mensaje/observaciones.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ShowAdminComentarioWindow(true);
                return;
            }

            Presenter.SaveComentario();
        }
       
        protected void BtnSelectComentario_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var IdSelectedComentario = btn.CommandArgument;

            Response.Redirect(string.Format("../Admin/FrmAdminComentarioRespuesta.aspx?ModuleId={0}&IdComentario={1}", IdModule, IdSelectedComentario));
        }

        protected void BtnAddUsuarioCopia_Click(object sender, EventArgs e)
        {
            var usuarioCopia = new DTO_ValueKey() { Id = IdUsuarioCopia, Value = wddUsuarioCopia.SelectedItem.Text };
            if (!ExistsInCopia(usuarioCopia))
                UsuariosCopia.Add(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);

            cpeCopiarUsuarios.Collapsed = false;
            ShowAdminComentarioWindow(true);
        }

        protected void BtnRemoveUsuarioCopia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdUsuarioCopiaSelected)) return;

            var usuarioCopia = UsuariosCopia.Where(x => x.Id == IdUsuarioCopiaSelected).SingleOrDefault();

            if (usuarioCopia != null)
                UsuariosCopia.Remove(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);

            cpeCopiarUsuarios.Collapsed = false;
            ShowAdminComentarioWindow(true);
        }

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivo.HasFile)
            {
                ShowAdminComentarioWindow(true);
                return;
            }

            var archivoAdjunto = new DTO_ValueKey();
            archivoAdjunto.Id = (ArchivosAdjuntos.Count + 1).ToString();
            archivoAdjunto.Value = fupAnexoArchivo.FileName;
            archivoAdjunto.ComplexValue = fupAnexoArchivo.FileBytes;

            ArchivosAdjuntos.Add(archivoAdjunto);
            LoadArchivosAdjuntos(ArchivosAdjuntos);

            ShowAdminComentarioWindow(true);
        }

        protected void BtnRemoveArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = ArchivosAdjuntos.Where(x => x.Id == IdArchivo).SingleOrDefault();

            if (archivo != null)
            {
                ArchivosAdjuntos.Remove(archivo);
                LoadArchivosAdjuntos(ArchivosAdjuntos);
            }

            ShowAdminComentarioWindow(true);
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = ArchivosAdjuntos.Where(x => x.Id == IdArchivo).SingleOrDefault();

            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");

            ShowAdminComentarioWindow(true);
        }

        #endregion

        #region Repeaters

        protected void RptComentariosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (ComentariosRespuesta)(e.Item.DataItem);
                // Bindind data                

                bool hasChild = false;

                var hddIdComentario = e.Item.FindControl("hddIdComentario") as HiddenField;
                if (hddIdComentario != null) hddIdComentario.Value = string.Format("{0}", item.IdComentario);

                var lblAsunto = e.Item.FindControl("lblAsunto") as Label;
                if (lblAsunto != null) lblAsunto.Text = string.Format("{0}", item.Asunto);

                var lblMensaje = e.Item.FindControl("lblMensaje") as Label;
                if (lblMensaje != null) lblMensaje.Text = string.Format("{0}", item.Comentario);

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgSelectComentario = e.Item.FindControl("imgSelectComentario") as ImageButton;
                if (imgSelectComentario != null)
                {
                    imgSelectComentario.CommandArgument = string.Format("{0}", item.IdComentario);
                }

                if (item.ComentariosRespuesta1 != null && item.ComentariosRespuesta1.Any())
                {
                    var rptChild = e.Item.FindControl("rptChildComentarios") as Repeater;
                    if (rptChild != null)
                    {
                        rptChild.DataSource = item.ComentariosRespuesta1;
                        rptChild.DataBind();
                    }

                    hasChild = true;
                }

                // Adicionando comportamiento de colapse para hijos
                var trParent = e.Item.FindControl("rowParent") as HtmlTableRow;
                if (trParent != null)
                {
                    var trChild = e.Item.FindControl("rowChild") as HtmlTableRow;
                    trChild.Visible = hasChild;
                }
            }
        }

        protected void RptComentariosAsociadosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (ComentariosRespuesta)(e.Item.DataItem);
                // Bindind data
                var lblAsunto = e.Item.FindControl("lblAsunto") as Label;
                if (lblAsunto != null) lblAsunto.Text = string.Format("{0}", item.Asunto);

                var lblMensjae = e.Item.FindControl("lblMensjae") as Label;
                if (lblMensjae != null) lblMensjae.Text = string.Format("{0}", item.Comentario);

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);
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

        void AddErrorMessages(List<string> messages)
        {
            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    var custVal = new CustomValidator();
                    custVal.IsValid = false;
                    custVal.ErrorMessage = msg;
                    custVal.EnableClientScript = false;
                    custVal.Display = ValidatorDisplay.None;
                    custVal.ValidationGroup = "vsComentarios";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        bool ExistsInCopia(DTO_ValueKey item)
        {
            foreach (var itm in UsuariosCopia)
            {
                if (itm.Id == item.Id)
                    return true;
            }

            return false;
        }

        void InitAdminComentario()
        {
            Asunto = string.Empty;
            Comentario = string.Empty;
            IdUsuarioDestino = UserSession.IdUser.ToString();
            ArchivosAdjuntos = new List<DTO_ValueKey>();
            UsuariosCopia = new List<DTO_ValueKey>();
            LoadUsuariosCopia(UsuariosCopia);
            LoadArchivosAdjuntos(ArchivosAdjuntos);
        }     

        #endregion

        #region IReclamoWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInit();
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminComentarioWindow(bool visible)
        {
            if (visible)
                mpeAdminSolucion.Show();
            else
                mpeAdminSolucion.Hide();
        }

        public void LoadComentarios(List<ComentariosRespuesta> items)
        {
            rptComentariosList.DataSource = items;
            rptComentariosList.DataBind();
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

        public void LoadUsuariosCopia(List<DTO_ValueKey> items)
        {
            lstUsuariosCopia.DataSource = items;
            lstUsuariosCopia.DataValueField = "Id";
            lstUsuariosCopia.DataTextField = "Value";
            lstUsuariosCopia.DataBind();
        }

        public void LoadUsuarioCopia(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddUsuarioCopia.DataSource = items;
            wddUsuarioCopia.DataTextField = "Nombres";
            wddUsuarioCopia.DataValueField = "IdUser";
            wddUsuarioCopia.DataBind();

            wddUsuarioCopia.SelectedIndex = 0;
        }

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();
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

        public string Asunto
        {
            get
            {
                return txtAsunto.Text;
            }
            set
            {
                txtAsunto.Text = value;
            }
        }

        public string Comentario
        {
            get
            {
                return txtObservaciones.Text;
            }
            set
            {
                txtObservaciones.Text = value;
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

        public List<DTO_ValueKey> UsuariosCopia
        {
            get
            {
                if (Session["AdminComentarios_UsuarioCopia"] == null)
                    Session["AdminComentarios_UsuarioCopia"] = new List<DTO_ValueKey>();

                return Session["AdminComentarios_UsuarioCopia"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminComentarios_UsuarioCopia"] = value;
            }
        }

        public string IdUsuarioCopia
        {
            get
            {
                return wddUsuarioCopia.SelectedValue;
            }
            set
            {
                wddUsuarioCopia.SelectedValue = value;
            }
        }

        public string IdUsuarioCopiaSelected
        {
            get
            {
                return lstUsuariosCopia.SelectedValue;
            }
            set
            {
                lstUsuariosCopia.SelectedValue = value;
            }
        }

        public List<DTO_ValueKey> ArchivosAdjuntos
        {
            get
            {
                if (Session["AdminComentario_ArchivosAdjuntos"] == null)
                    Session["AdminComentario_ArchivosAdjuntos"] = new List<DTO_ValueKey>();

                return Session["AdminComentario_ArchivosAdjuntos"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminComentario_ArchivosAdjuntos"] = value;
            }
        }

        #endregion

        #endregion
    }
}