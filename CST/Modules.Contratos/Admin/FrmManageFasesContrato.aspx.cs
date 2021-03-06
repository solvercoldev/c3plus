﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Application.MainModule.Contratos.DTO;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Contratos.UI;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;
using System.Web.UI.HtmlControls;
using Application.Core;

namespace Modules.Contratos.Admin
{
    public partial class FrmManageFasesContrato : ViewPage<ManageFasesContratoPresenter, IManageFasesContratoView>, IManageFasesContratoView
    {
        #region Members

        public const string ROOTUC = "../UserControls/";

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        public string FromPageAux
        {
            get
            {
                return Request.QueryString["fromaux"];
            }
        }

        public string IdFrom
        {
            get
            {
                return Request.QueryString["idfrom"];
            }
        }

        public string IdFromAux
        {
            get
            {
                return Request.QueryString["idfromaux"];
            }
        }

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

        string LastLoadedControl
        {
            get
            {
                if (Session["WuCFases_LastLoaded"] == null)
                    Session["WuCFases_LastLoaded"] = string.Empty;

                return Session["WuCFases_LastLoaded"] as string;
            }
            set
            {
                Session["WuCFases_LastLoaded"] = value;
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Administrar Fases Contrato: {0}", NombreContrato));
            LoadUserControl();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Menu

        protected void MnuItemClick(object sender, MenuEventArgs e)
        {
            LastLoadedControl = e.Item.Value;
            LoadUserControl();
        }

        #endregion

        #region Buttons

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato)); 
        }

        protected void BtnAddNovedad_Click(object sender, EventArgs e)
        {
            var btnAccion = (Button)sender;
            TipoOperacion = btnAccion.CommandArgument;
            InitNovedad();
            ShowAdminWindow(true);
        }

        protected void BtnAddFase_Click(object sender, EventArgs e)
        {
            TipoOperacion = "Adición Fase";
            InitAddFase();
            ShowAdminFase(true);
        }

        protected void BtnSaveNovedad_Click(object sender, EventArgs e)
        {
            Presenter.SaveNovedad();

            LoadUserControl();
        }

        protected void BtnSaveFase_Click(object sender, EventArgs e)
        {
            switch (PeriodoNuevaFase)
            {
                case "Evaluación":
                    Presenter.AddFaseEvaluacion();
                    break;
                case "Exp.Posterior":
                    Presenter.AddFaseExpPosterior();
                    break;
                case "Producción":
                    Presenter.AddFaseProduccion();
                    break;
            }
            LoadUserControl();
        }
        
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }
        }

        #endregion

        #region RadioButtons

        protected void RdbTipoNuevaFase_IndexChanged(Object sender, EventArgs e)
        {
            trFaseEvaluacionNuevaFase.Visible = false;

            switch (PeriodoNuevaFase)
            {
                case "Evaluación":                    
                    txtFechaInicioNuevaFase.Visible = true;
                    lblFechaInicialNuevaFase.Visible = false;
                    LoadInfoAdminFase();
                    break;
                case "Exp.Posterior":
                    txtFechaInicioNuevaFase.Visible = false;
                    lblFechaInicialNuevaFase.Visible = true;
                    LoadInfoFaseActual();
                    break;
                case "Producción":
                    txtFechaInicioNuevaFase.Visible = false;
                    lblFechaInicialNuevaFase.Visible = true;
                    LoadFasesEvaluacion();
                    LoadInfoFaseEvaluacion();
                    break;
            }
            ShowAdminFase(true);
        }

        #endregion

        #region TextBoxes

        protected void TxtFechaInicioNuevaFase_TextChanged(Object sender, EventArgs e)
        {
            cextFechaFinNuevaFase.StartDate = FechaInicioNuevaFase.AddDays(1);
            FechaFinNuevaFase = FechaInicioNuevaFase.AddDays(1);
            ShowAdminFase(true);
        }

        #endregion

        #region DropDownList

        protected void DdlFases_IndexChanged(Object sender, EventArgs e)
        {
            var fase = FasesAdminList.Where(x => x.IdFase == IdFase).First();

            if (TipoOperacion != "CorrecciónFechaFin")
            {
                cexTxtFechaNovedad.StartDate = fase.FechaFinalizacion.AddDays(1);
                FechaFinalExtension = fase.FechaFinalizacion.AddDays(1);
            }
            else
            {
                cexTxtFechaNovedad.StartDate = fase.FechaInicio.AddDays(1);
                FechaFinalExtension = fase.FechaInicio.AddDays(1);
            }

            if (TipoOperacion == "Unificación")
            {
                var fasesFin = FasesAdminList.Where(x => x.NumeroFase == fase.NumeroFase + 1);

                if (fasesFin.Any())
                {
                    var faseFin = fasesFin.First();

                    lblFaseUnificacion.Text = faseFin.Nombre;
                    lblPeriodoUnifiacion.Text = string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", fase.FechaInicio, faseFin.FechaFinalizacion);
                    FechaFinalExtension = faseFin.FechaFinalizacion;
                }
            }

            ShowAdminWindow(true);
        }

        protected void DdlFasesEvaluacion_IndexChanged(Object sender, EventArgs e)
        {
            var faseEvaluacion = FasesAdminList.Where(x => x.IdFase == IdFaseEvaluacion);

            if (faseEvaluacion.Any())
            {
                cextFechaInicioNuevaFase.StartDate = faseEvaluacion.FirstOrDefault().FechaInicio.AddDays(1);
                cextFechaFinNuevaFase.StartDate = faseEvaluacion.FirstOrDefault().FechaInicio.AddDays(2);

                FechaInicioNuevaFase = faseEvaluacion.FirstOrDefault().FechaInicio.AddDays(1);
                FechaFinNuevaFase = faseEvaluacion.FirstOrDefault().FechaInicio.AddDays(2);
            }

            ShowAdminFase(true);
        }

        #endregion

        #region Repeaters

        protected void RptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (Fases)(e.Item.DataItem);
                // Bindind data

                var hddIdFase = e.Item.FindControl("hddIdFase") as HiddenField;
                if (hddIdFase != null) hddIdFase.Value = string.Format("{0}", item.IdFase);

                var lblPeriodo = e.Item.FindControl("lblPeriodo") as Label;
                if (lblPeriodo != null) lblPeriodo.Text = string.Format("{0}", item.Periodo);

                var lblFase = e.Item.FindControl("lblFase") as Label;
                if (lblFase != null) lblFase.Text = string.Format("{0}", item.NumeroFase);

                if (item.NumFaseUnificada.HasValue)
                {
                    lblFase.Text = string.Format("{0} y {1}", item.NumeroFase, item.NumFaseUnificada);
                }

                var lblFechaInicio = e.Item.FindControl("lblFechaInicio") as Label;
                if (lblFechaInicio != null) lblFechaInicio.Text = string.Format("{0:dd/MM/yyyy}", item.FechaInicio);

                var lblFechaFin = e.Item.FindControl("lblFechaFin") as Label;
                if (lblFechaFin != null) lblFechaFin.Text = string.Format("{0:dd/MM/yyyy}", item.FechaFinalizacion);

                var lblDuracion = e.Item.FindControl("lblDuracion") as Label;
                if (lblDuracion != null) lblDuracion.Text = string.Format("{0}", item.DuracionMeses);

                var trFase = e.Item.FindControl("trFase") as HtmlTableRow;

                var imgFase = e.Item.FindControl("imgFase") as Image;
                if (imgFase != null)
                {
                    if (DateTime.Now >= item.FechaInicio && DateTime.Now <= item.FechaFinalizacion)
                    {
                        imgFase.ImageUrl = "~/Resources/images/sandglass.png";
                        imgFase.Focus();
                        item.FaseActiva = true;

                        if (trFase != null)
                        {
                            trFase.Attributes.Add("style", "background-color: #6ec138");
                        }

                        btnUnificar.Visible = !item.NumFaseUnificada.HasValue;
                    }
                    else if (DateTime.Now > item.FechaFinalizacion)
                    {
                        imgFase.ImageUrl = "~/Resources/images/check.png";
                    }
                    else if (DateTime.Now < item.FechaInicio)
                    {
                        imgFase.ImageUrl = "~/Resources/images/calendar.png";
                    }
                }

                var imgeUnify = e.Item.FindControl("imgeUnify") as Image;
                if (imgeUnify != null && item.NumFaseUnificada.HasValue)
                {
                    imgeUnify.Visible = true;
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
                    custVal.ValidationGroup = "vgGeneral";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        void LoadUserControl()
        {
            var controlPath = LastLoadedControl;
            var idUc = "";

            if (!string.IsNullOrEmpty(controlPath))
            {
                foreach (MenuItem mi in mnuSecciones.Items)
                {
                    if (mi.Value == controlPath)
                        mi.Selected = true;
                }
            }

            if (string.IsNullOrEmpty(controlPath))
            {
                controlPath = "WuCAdminNovedadesFasesContrato.ascx";
                if (mnuSecciones.Items.Count > 0)
                    mnuSecciones.Items[0].Selected = true;
            }
            if (string.IsNullOrEmpty(controlPath)) return;

            if (!controlPath.Contains('/'))
            {
                idUc = controlPath.Split('.')[0];
                controlPath = string.Format("{0}{1}", ROOTUC, controlPath);
            }
            else
            {
                idUc = "uc";
            }

            phlContent.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = idUc;
            phlContent.Controls.Add(uc);

            if (uc is IContratoWebUserControl)
            {
                ((IContratoWebUserControl)uc).LoadControlData();
            }
        }

        public void LoadInitControl()
        {
            var controlPath = "";
            var idUc = "";

            controlPath = string.Format("{0}WuCAdminNovedadesFasesContrato.ascx", ROOTUC);
            idUc = "WuCAdminNovedadesFasesContrato";
        }

        void InitNovedad()
        {
            var fasesList = new List<Fases>();

            var faseIni = new Fases();
            var faseFin = new Fases();

            var fases = FasesAdminList.Where(x => DateTime.Now >= x.FechaInicio && DateTime.Now <= x.FechaFinalizacion);

            if (TipoOperacion == "CorrecciónFechaFin")
            {
                fases = FasesAdminList.Where(x => DateTime.Now >= x.FechaFinalizacion || DateTime.Now >= x.FechaInicio);
            }

            FechaFinalExtension = DateTime.Now;
            Descripcion = string.Empty;
            
            if (fases.Any())
            {
                fasesList = fases.ToList();

                faseIni = fasesList[0];
                cexTxtFechaNovedad.StartDate = faseIni.FechaFinalizacion.AddDays(1);
                FechaFinalExtension = faseIni.FechaFinalizacion.AddDays(1);

                if (TipoOperacion == "CorrecciónFechaFin")
                {
                    cexTxtFechaNovedad.StartDate = faseIni.FechaInicio.AddDays(1);
                    FechaFinalExtension = faseIni.FechaInicio.AddDays(1);
                }
            }

            ddlFaseOperacion.DataSource = fasesList;
            ddlFaseOperacion.DataTextField = "Nombre";
            ddlFaseOperacion.DataValueField = "IdFase";
            ddlFaseOperacion.DataBind();

            trFechaExtension.Visible = true;
            trFaseunificacion.Visible = false;
            trPeridoUnificacion.Visible = false;

            if (TipoOperacion == "Unificación")
            {
                trFechaExtension.Visible = false;
                trFaseunificacion.Visible = true;
                trPeridoUnificacion.Visible = true;

                var fasesFin = FasesAdminList.Where(x => x.NumeroFase == faseIni.NumeroFase + 1);

                if (fasesFin.Any())
                {
                    faseFin = fasesFin.First();

                    lblFaseUnificacion.Text = faseFin.Nombre;
                    lblPeriodoUnifiacion.Text = string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", faseIni.FechaInicio, faseFin.FechaFinalizacion);

                    FechaFinalExtension = faseFin.FechaFinalizacion;
                }
            }
        }

        private void InitAddFase()
        {
            ObservacionesNuevaFase = string.Empty;
            LoadPeriodoNuevaFase();
        }

        void LoadPeriodoNuevaFase()
        {
            var periodoNuevaFase = new List<DTO_ValueKey>();
            bool CanExploratorio = false;
            bool CanProduccion = false;
            trFaseEvaluacionNuevaFase.Visible = false;
            lblFechaInicialNuevaFase.Visible = false;
            txtFechaInicioNuevaFase.Visible = true;
            periodoNuevaFase.Add(new DTO_ValueKey() { Id = "Evaluación", Value = "Evaluación" });

            // Verificando si el contrato se encuentra en la ultima fase y es de tipo exploratorio
            var fasesExploratorio = FasesAdminList.Where(x => (DateTime.Now >= x.FechaInicio && DateTime.Now <= x.FechaFinalizacion)
                                                  && x.Periodo.Contains("Exp")
                                                  ).OrderByDescending(x => x.IdFase);

            var fasesExploratorioPosterior = FasesAdminList.Where(x =>  x.Periodo.Contains("Exp.Posterior")
                                                  );

            CanExploratorio = fasesExploratorio.Any() && fasesExploratorioPosterior.Count() < TotalFasesExpPosterior;

            // Verificando si el contrato tiene fases de Evaluacion sin produccion
            var fasesEvaluacion = FasesAdminList.Where(x => x.IsActive && x.TipoEvalFase == 1 && !x.TieneFaseProduccion);
            CanProduccion = fasesEvaluacion.Any();

            // Cargando Vista
            cextFechaInicioNuevaFase.StartDate = FechaEfectivaContrato.AddDays(1);
            cextFechaFinNuevaFase.StartDate = FechaEfectivaContrato.AddDays(2);
            FechaInicioNuevaFase = FechaEfectivaContrato.AddDays(1);
            FechaFinNuevaFase = FechaEfectivaContrato.AddDays(2);

            if (CanExploratorio)
            {
                periodoNuevaFase.Add(new DTO_ValueKey() { Id = "Exp.Posterior", Value = "Exp.Posterior" });
                var faseExp = fasesExploratorio.FirstOrDefault();

                FechaInicioNuevaFase = faseExp.FechaInicio.AddDays(1);
                FechaFinNuevaFase = faseExp.FechaFinalizacion;
            }

            if (CanProduccion)
            {
                periodoNuevaFase.Add(new DTO_ValueKey() { Id = "Producción", Value = "Producción" });                
            }

            rdbTipoNuevaFase.DataSource = periodoNuevaFase;
            rdbTipoNuevaFase.DataTextField = "Value";
            rdbTipoNuevaFase.DataValueField = "Id";
            rdbTipoNuevaFase.DataBind();


            rdbTipoNuevaFase.SelectedIndex = 0;
        }

        void LoadFasesEvaluacion()
        {
            var fasesEvaluacion = new List<Fases>();
            var faseIni = new Fases();
            var fases = FasesAdminList.Where(x => x.TipoEvalFase == 1 && x.IsActive && !x.TieneFaseProduccion);

            if (fases.Any())
            {
                fasesEvaluacion = fases.ToList();
                faseIni = fasesEvaluacion[0];

                cextFechaInicioNuevaFase.StartDate = faseIni.FechaFinalizacion.AddDays(1);
                cextFechaFinNuevaFase.StartDate = faseIni.FechaFinalizacion.AddDays(1);
                FechaInicioNuevaFase = faseIni.FechaFinalizacion.AddDays(1);
                FechaFinNuevaFase = FechaInicioNuevaFase.AddYears(1);

                lblFechaInicialNuevaFase.Visible = true;
                txtFechaInicioNuevaFase.Visible = false;
            }

            ddlFaseEvaluacion.DataSource = fasesEvaluacion;
            ddlFaseEvaluacion.DataTextField = "Nombre";
            ddlFaseEvaluacion.DataValueField = "IdFase";
            ddlFaseEvaluacion.DataBind(); ;

            trFaseEvaluacionNuevaFase.Visible = true;
        }

        void LoadInfoAdminFase()
        {
            // Verificando si el contrato se encuentra en la ultima fase y es de tipo exploratorio
            var fasesExploratorio = FasesAdminList.Where(x => (DateTime.Now >= x.FechaInicio && DateTime.Now <= x.FechaFinalizacion)
                                                  && x.Periodo.Contains("Exp")
                                            ).OrderByDescending(x => x.IdFase);
            // Cargando Vista
            cextFechaInicioNuevaFase.StartDate = FechaEfectivaContrato.AddDays(1);
            cextFechaFinNuevaFase.StartDate = FechaEfectivaContrato.AddDays(1);
            FechaInicioNuevaFase = FechaEfectivaContrato.AddDays(1);
            FechaFinNuevaFase = FechaEfectivaContrato.AddDays(1);

            if (fasesExploratorio.Any())
            {
                var faseExp = fasesExploratorio.FirstOrDefault();
                FechaInicioNuevaFase = faseExp.FechaInicio.AddDays(1);
                FechaFinNuevaFase = faseExp.FechaFinalizacion;
            }
        }

        void LoadInfoFaseEvaluacion()
        {
            var faseExploratoria = FasesAdminList.Where(x => x.IdFase == IdFaseEvaluacion);

            if (faseExploratoria.Any())
            {
                cextFechaInicioNuevaFase.StartDate = faseExploratoria.FirstOrDefault().FechaFinalizacion.AddDays(1);
                cextFechaFinNuevaFase.StartDate = faseExploratoria.FirstOrDefault().FechaFinalizacion.AddDays(1);

                FechaInicioNuevaFase = faseExploratoria.FirstOrDefault().FechaFinalizacion.AddDays(1);
                FechaFinNuevaFase = faseExploratoria.FirstOrDefault().FechaFinalizacion.AddDays(1);
            }
        }

        void LoadInfoFaseActual()
        {
            var faseActual = FasesAdminList.Where(x => x.Periodo.Contains("Exp")).OrderByDescending(x => x.IdFase);

            if (faseActual.Any())
            {
                cextFechaInicioNuevaFase.StartDate = faseActual.FirstOrDefault().FechaFinalizacion.AddDays(1);
                cextFechaFinNuevaFase.StartDate = faseActual.FirstOrDefault().FechaFinalizacion.AddDays(1);

                FechaInicioNuevaFase = faseActual.FirstOrDefault().FechaFinalizacion.AddDays(1);
                FechaFinNuevaFase = faseActual.FirstOrDefault().FechaFinalizacion.AddDays(1);
            }
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminWindow(bool visible)
        {
            if (visible)
                mpeAdminNovedad.Show();
            else
                mpeAdminNovedad.Hide();
        }

        public void ShowAdminFase(bool visible)
        {
            if (visible)
                mpAdminNuevaFase.Show();
            else
                mpAdminNuevaFase.Hide();
        }
        
        public void LoadFases(List<Fases> items)
        {
            if (items.Any())
                items = items.OrderBy(x => x.FechaInicio).ToList();

            rptFases.DataSource = items;
            rptFases.DataBind();

            FasesAdminList = items;
        }

        public void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones)
        {
            mnuSecciones.Items.Clear();
            foreach (var seccione in from tab in secciones select tab)
            {
                var opcion = new MenuItem
                {
                    Text = seccione.Titulo,
                    Value =
                        (string.IsNullOrEmpty(IsEdit))
                            ? seccione.PathEdit
                            : seccione.PathPreview
                };
                mnuSecciones.Items.Add(opcion);
            }
            mnuSecciones.Items[0].Selected = true;
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

        public List<Fases> FasesAdminList
        {
            get
            {
                return Session["ManageFasesContrato_FasesAdminList"] as List<Fases>;
            }
            set
            {
                Session["ManageFasesContrato_FasesAdminList"] = value;
            }
        }

        public DateTime FechaFinalExtension
        {
            get { return Convert.ToDateTime(txtFechaNovedad.Text); }
            set { txtFechaNovedad.Text = value.ToString("dd/MM/yyyy"); }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public int IdFase
        {
            get { return Convert.ToInt32(ddlFaseOperacion.SelectedValue); }
            set { ddlFaseOperacion.SelectedValue = value.ToString(); }
        }

        public int IdFaseEvaluacion
        {
            get { return Convert.ToInt32(ddlFaseEvaluacion.SelectedValue); }
            set { ddlFaseEvaluacion.SelectedValue = value.ToString(); }
        }

        public string PeriodoNuevaFase
        {
            get { return rdbTipoNuevaFase.SelectedValue; }
            set { rdbTipoNuevaFase.SelectedValue = value; }
        }

        public string ObservacionesNuevaFase
        {
            get { return txtObservacionesNuevaFase.Text; }
            set { txtObservacionesNuevaFase.Text = value; }
        }

        public DateTime FechaInicioNuevaFase
        {
            get { return Convert.ToDateTime(txtFechaInicioNuevaFase.Text); }
            set 
            {
                txtFechaInicioNuevaFase.Text = value.ToString("dd/MM/yyyy");
                lblFechaInicialNuevaFase.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public DateTime FechaFinNuevaFase
        {
            get { return Convert.ToDateTime(txtFechaFinNuevaFase.Text); }
            set { txtFechaFinNuevaFase.Text = value.ToString("dd/MM/yyyy"); }
        }

        public DateTime FechaFinContrato
        {
            get
            {
                return Convert.ToDateTime(ViewState["ManageFases_FechaFinContrato"]);
            }
            set
            {
                ViewState["ManageFases_FechaFinContrato"] = value;
            }
        }

        public DateTime FechaEfectivaContrato
        {
            get
            {
                return Convert.ToDateTime(ViewState["ManageFases_FechaEfectivaContrato"]);
            }
            set
            {
                ViewState["ManageFases_FechaEfectivaContrato"] = value;
            }
        }

        public int TotalFasesExpPosterior
        {
            get
            {
                return Convert.ToInt32(ViewState["ManageFases_TotalFasesExpPosterior"]);
            }
            set
            {
                ViewState["ManageFases_TotalFasesExpPosterior"] = value;
            }
        }

        public bool EnableActions
        {
            get
            {
                return pnlContainerActionButtons.Visible;
            }
            set
            {
                pnlContainerActionButtons.Visible = value;
            }
        }

        #endregion

        #endregion       
    }
}