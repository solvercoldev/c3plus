using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Domain.MainModules.Entities;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.IoC;

namespace Presenters.Contratos.Presenters
{
    public class ManageFasesContratoPresenter : Presenter<IManageFasesContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        ISfFasesManagementServices _fasesService;
        readonly ISfTBL_Admin_SeccionesManagementServices _seccionesServices;
        readonly ISfNovedadesFaseManagementServices _novedadesFaseService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly IContratosAdoService _adoService;
        readonly ISfLogContratosManagementServices _log;

        public ManageFasesContratoPresenter(ISfContratosManagementServices contratoService,
                                 ISfFasesManagementServices fasesService,
                                 ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                 ISfNovedadesFaseManagementServices novedadesFaseService,
                                 ISfTBL_Admin_OptionListManagementServices optionListService,
                                 IContratosAdoService adoService,
                                 ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _fasesService = fasesService;
            _seccionesServices = seccionesServices;
            _novedadesFaseService = novedadesFaseService;
            _optionListService = optionListService;
            _adoService = adoService;
            _log = log;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInit();
            InitView();
        }

        public void LoadInit()
        {
            LoadContrato();
            CargarSecciones();
            LoadOptionList();

            View.EnableActions = View.UserSession.IsInRole("Administrador");
        }

        void InitView()
        {
        }

        void LoadContrato()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(View.IdContrato));
                if (contrato != null)
                {
                    View.NombreContrato = contrato.Nombre;
                    View.FechaEfectivaContrato = contrato.FechaInicio;
                    View.FechaFinContrato = contrato.FechaTerminacion;

                    LoadFases();
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadFases()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                _fasesService = IoC.Resolve<ISfFasesManagementServices>();
                var fases = _fasesService.GetFasesByContrato(Convert.ToInt32(View.IdContrato));
                View.LoadFases(fases);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadOptionList()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("TotalFasesExpPosterior", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.TotalFasesExpPosterior = Convert.ToInt32(op.Value);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void CargarSecciones()
        {
            try
            {
                var secciones = _seccionesServices.ListadoSeccionesPorModulo(1);
                View.LoadSecciones(secciones);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void SaveNovedad()
        {
            try
            {
                var model = GetModel();
                var fase = _fasesService.FindById(View.IdFase);
                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                model.Descripcion = string.Format("{0} - {1}", model.Descripcion, fase.Nombre);

                _novedadesFaseService.Add(model);

                switch (View.TipoOperacion)
                {
                    case "Extensión":
                        ExtenderFase();
                        break;
                    case "Prorroga":
                        ProrrogarFase();
                        break;
                    case "Unificación":
                        UnificarFase();
                        break;
                    case "CorrecciónFechaFin":
                        CorregirFechaFinFase();
                        break;
                }

                var log = GetLog();
                _log.Add(log);

                contrato.ModifiedBy = View.UserSession.IdUser;
                contrato.ModifiedOn = DateTime.Now;

                _contratoService.Modify(contrato);

                LoadFases();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddFaseExpPosterior()
        {
            try
            {
                var fase = new Fases();
                var fasesExploratorio = View.FasesAdminList.Where(x => (DateTime.Now >= x.FechaInicio && DateTime.Now <= x.FechaFinalizacion)                                                  
                                                  && x.Periodo.Contains("Exp")
                                            ).OrderByDescending(x => x.IdFase);

                if (fasesExploratorio.Any())
                {
                    fase.IdContrato = Convert.ToInt32(View.IdContrato);
                    fase.Periodo = "Exp.Posterior";
                    fase.NumeroFase = fasesExploratorio.Max(x => x.NumeroFase) + 1;
                    fase.Nombre = string.Format("Exp.Posterior {0}", fase.NumeroFase);
                    fase.FechaInicio = View.FechaInicioNuevaFase;
                    fase.FechaFinalizacion = View.FechaFinNuevaFase;
                    fase.DuracionMeses = DiffMonths(fase.FechaInicio, fase.FechaFinalizacion);
                    fase.TipoEvalFase = 0;
                    fase.Estado = 1;
                    fase.Grupo = 1;
                    fase.Aplica = true;
                    fase.IsActive = true;
                    fase.CreateBy = View.UserSession.IdUser;
                    fase.CreateOn = DateTime.Now;
                    fase.ModifiedBy = View.UserSession.IdUser;
                    fase.ModifiedOn = DateTime.Now;

                    _fasesService.Add(fase);

                    AddNovedadNuevaFase(fase);

                    LoadContrato();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddFaseEvaluacion()
        {
            try
            {                
                var fase = new Fases();

                var grupoId = View.FasesAdminList.Max(x => x.Grupo);
                var fasesEvaluacion = View.FasesAdminList.Where(x => x.Periodo == "Evaluación" && x.IsActive);

                fase.IdContrato = Convert.ToInt32(View.IdContrato);
                fase.Periodo = "Evaluación";
                fase.NumeroFase = fasesEvaluacion.Any() ? fasesEvaluacion.Max(x => x.NumeroFase) + 1 : 1;
                fase.Nombre = string.Format("Evaluación {0}", fase.NumeroFase);
                fase.FechaInicio = View.FechaInicioNuevaFase;
                fase.FechaFinalizacion = View.FechaFinNuevaFase;
                fase.DuracionMeses = DiffMonths(fase.FechaInicio, fase.FechaFinalizacion);
                fase.TipoEvalFase = 1;
                fase.Estado = 1;
                fase.Grupo = grupoId + 1;
                fase.Aplica = true;
                fase.IsActive = true;
                fase.CreateBy = View.UserSession.IdUser;
                fase.CreateOn = DateTime.Now;
                fase.ModifiedBy = View.UserSession.IdUser;
                fase.ModifiedOn = DateTime.Now;

                _fasesService.Add(fase);

                AddNovedadNuevaFase(fase);

                LoadContrato();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddFaseProduccion()
        {
            try
            {
                var fase = new Fases();
                var fasesEvaluacion = _fasesService.FindById(View.IdFaseEvaluacion);                

                if (fasesEvaluacion != null)
                {
                    fase.IdContrato = Convert.ToInt32(View.IdContrato);
                    fase.Periodo = "Producción";
                    fase.NumeroFase = fasesEvaluacion.NumeroFase;
                    fase.Nombre = string.Format("Producción {0}", fase.NumeroFase);
                    fase.FechaInicio = View.FechaInicioNuevaFase;
                    fase.FechaFinalizacion = View.FechaFinNuevaFase;
                    fase.DuracionMeses = DiffMonths(fase.FechaInicio, fase.FechaFinalizacion);
                    fase.TipoEvalFase = 0;
                    fase.Estado = 1;
                    fase.Grupo = fasesEvaluacion.Grupo;
                    fase.Aplica = true;
                    fase.IsActive = true;
                    fase.CreateBy = View.UserSession.IdUser;
                    fase.CreateOn = DateTime.Now;
                    fase.ModifiedBy = View.UserSession.IdUser;
                    fase.ModifiedOn = DateTime.Now;

                    _fasesService.Add(fase);

                    fasesEvaluacion.TieneFaseProduccion = true;
                    _fasesService.Modify(fasesEvaluacion);

                    AddNovedadNuevaFase(fase);

                    LoadContrato();
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void AddNovedadNuevaFase(Fases fase)
        {
            var novedadFase = new NovedadesFase();
            var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

            novedadFase.IdFase = fase.IdFase;
            novedadFase.TipoNovedad = View.TipoOperacion;
            novedadFase.Descripcion = string.Format("{0} - {1}", View.ObservacionesNuevaFase, fase.Nombre);
            novedadFase.FechaNovedad = View.FechaInicioNuevaFase;
            novedadFase.FechaInicioAnterior = DateTime.Now;
            novedadFase.FechaInicioPosterior = DateTime.Now;
            novedadFase.FechaFinAnterior = DateTime.Now;
            novedadFase.FechaFinPosterior = DateTime.Now;
            novedadFase.DiasDesplazamiento = 0;
            novedadFase.NombreFaseAnterior = string.Empty;
            novedadFase.NombreFasePosterior = string.Empty;
            novedadFase.NumFaseUnion = 0;
            novedadFase.IsActive = true;
            novedadFase.CreateBy = View.UserSession.IdUser;
            novedadFase.CreateOn = DateTime.Now;
            novedadFase.ModifiedBy = View.UserSession.IdUser;
            novedadFase.ModifiedOn = DateTime.Now;

            _novedadesFaseService.Add(novedadFase);

            var log = GetLog();
            log.Descripcion = string.Format("El usuario [{0}] ha ingresado una novedad de fase de tipo [{1}]. Comentarios: [{2}]", View.UserSession.Nombres, View.TipoOperacion, View.ObservacionesNuevaFase);
            _log.Add(log);

            contrato.ModifiedBy = View.UserSession.IdUser;
            contrato.ModifiedOn = DateTime.Now;

            if (fase.FechaFinalizacion > contrato.FechaTerminacion)
                contrato.FechaTerminacion = fase.FechaFinalizacion;

            _contratoService.Modify(contrato);
        }

        void ExtenderFase()
        {
            _adoService.ExtenderFase(View.IdFase, View.FechaFinalExtension);
        }

        void ProrrogarFase()
        {
            _adoService.ProrrogarFase(View.IdFase, View.FechaFinalExtension);
        }

        void UnificarFase()
        {
            _adoService.UnificarFase(View.IdFase, View.FechaFinalExtension);
        }

        void CorregirFechaFinFase()
        {
            _adoService.CorregirFechaFinFase(View.IdFase, View.FechaFinalExtension);
        }

        NovedadesFase GetModel()
        {
            var model = new NovedadesFase();

            model.IdFase = View.IdFase;
            model.TipoNovedad = View.TipoOperacion;
            model.Descripcion = View.Descripcion;
            model.FechaNovedad = View.FechaFinalExtension;
            model.FechaInicioAnterior = DateTime.Now;
            model.FechaInicioPosterior = DateTime.Now;
            model.FechaFinAnterior = DateTime.Now;
            model.FechaFinPosterior = DateTime.Now;
            model.DiasDesplazamiento = 0;
            model.NombreFaseAnterior = string.Empty;
            model.NombreFasePosterior = string.Empty;
            model.NumFaseUnion = 0;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

        LogContratos GetLog()
        {
            var model = new LogContratos();

            model.IdLog = Guid.NewGuid();
            model.IdContrato = Convert.ToInt32(View.IdContrato);
            model.Descripcion = string.Format("El usuario [{0}] ha ingresado una novedad de fase de tipo [{1}]. Comentarios: [{2}]", View.UserSession.Nombres, View.TipoOperacion, View.Descripcion);
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}