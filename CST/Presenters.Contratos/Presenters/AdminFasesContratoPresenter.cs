using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using System.Collections.Generic;
using Application.MainModule.Contratos.DTO;
using Domain.MainModules.Entities;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Contratos.Presenters
{
    public class AdminFasesContratoPresenter : Presenter<IAdminFasesContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfFasesManagementServices _fasesService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly IContratosAdoService _adoService;
        readonly ISfLogContratosManagementServices _log;

        public AdminFasesContratoPresenter(ISfContratosManagementServices contratoService,
                                           ISfFasesManagementServices fasesService,
                                           ISfTBL_Admin_OptionListManagementServices optionListService,
                                           IContratosAdoService adoService,
                                           ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _fasesService = fasesService;
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
            LoadOptionListValues();

            View.CanSave = View.UserSession.IsInRole("Administrador");
            View.CanGenerateFases = View.UserSession.IsInRole("Administrador");
        }

        void InitView()
        {
            View.NumeroFases = 6;
            View.FasesContrato = new List<Dto_FaseContrato>();
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
                    View.NumeroContrato = contrato.NumeroContrato;
                    View.Empresa = contrato.Empresas.RazonSocial;
                    View.FechaFirma = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaFirma);
                    View.FechaEfectiva = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaInicio);
                    View.FechaFirmaInit = contrato.FechaFirma;
                    View.FechaEfectivaInit = contrato.FechaInicio;
                    View.Periodo = string.Format("Sin Definir");
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadOptionListValues()
        {
            var duracionList = new List<DTO_ValueKey>();
            var tipoFaseList = new List<DTO_ValueKey>();

            try
            {
                var duracionContrato = _optionListService.ObtenerOpcionBykeyModuleId("DuracionContrato", Convert.ToInt32(View.IdModule));
                var tipoFase = _optionListService.ObtenerOpcionBykeyModuleId("TipoFase", Convert.ToInt32(View.IdModule));
                var minMesesFaseCero = _optionListService.ObtenerOpcionBykeyModuleId("MinMesesFaseCero", Convert.ToInt32(View.IdModule));
                var maxMesesFaseCero = _optionListService.ObtenerOpcionBykeyModuleId("MaxMesesFaseCero", Convert.ToInt32(View.IdModule));
                var minMesesFaseExploratorio = _optionListService.ObtenerOpcionBykeyModuleId("MinMesesFaseExploratorio", Convert.ToInt32(View.IdModule));
                var maxMesesFaseExploratorio = _optionListService.ObtenerOpcionBykeyModuleId("MaxMesesFaseExploratorio", Convert.ToInt32(View.IdModule));
                var maxTotalMesesFaseExploratorio = _optionListService.ObtenerOpcionBykeyModuleId("MaxTotalMesesFaseExploratorio", Convert.ToInt32(View.IdModule));                

                if (duracionContrato != null)
                {
                    var infoDuracion = duracionContrato.Value.Split('|');
                    foreach (var itm in infoDuracion)
                    {
                        duracionList.Add(new DTO_ValueKey() { Id = itm, Value = itm });
                    }
                }

                if (tipoFase != null)
                {
                    var infoFases = tipoFase.Value.Split('|');
                    foreach (var itm in infoFases)
                    {
                        tipoFaseList.Add(new DTO_ValueKey() { Id = itm, Value = itm });
                    }
                }

                if (minMesesFaseCero != null)
                {
                    View.MinDuracionFaseCero = Convert.ToInt32(minMesesFaseCero.Value);                     
                }
                if (maxMesesFaseCero != null)
                {
                    View.MaxDuracionFaseCero = Convert.ToInt32(maxMesesFaseCero.Value);
                }

                if (minMesesFaseExploratorio != null)
                {
                    View.MinDuracionFaseExploratorio = Convert.ToInt32(minMesesFaseExploratorio.Value);
                }
                if (maxMesesFaseExploratorio != null)
                {
                    View.MaxDuracionFaseExploratorio = Convert.ToInt32(maxMesesFaseExploratorio.Value);
                }

                if (maxTotalMesesFaseExploratorio != null)
                {
                    View.MaxTotalDuracionFaseExploratorio = Convert.ToInt32(maxTotalMesesFaseExploratorio.Value);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.TiposFase = tipoFaseList;
            View.DuracionContrato = duracionList;
        }

        public void SaveFases()
        {
            try
            {
                var fasesView = View.FasesContrato;
                var auxDate = DateTime.Now;

                if (!CheckFasesContrato())
                    return;

                foreach (var f in fasesView)
                {
                    var fase = new Fases();
                    fase.IdContrato = Convert.ToInt32(View.IdContrato);
                    fase.Periodo = f.Periodo;
                    fase.NumeroFase = f.FaseId;
                    fase.Nombre = f.Fase;
                    fase.FechaInicio = f.FechaInicio;
                    fase.FechaFinalizacion = f.FechaFin;
                    fase.DuracionMeses = f.DuracionFase;
                    fase.Estado = 1;
                    fase.Grupo = f.Grupo;
                    fase.Aplica = true;
                    fase.IsActive = true;
                    fase.CreateBy = View.UserSession.IdUser;
                    fase.CreateOn = DateTime.Now;
                    fase.ModifiedBy = View.UserSession.IdUser;
                    fase.ModifiedOn = DateTime.Now;

                    auxDate = f.FechaFin;

                    _fasesService.Add(fase);
                }

                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));
                if (contrato != null)
                {
                    contrato.FechaTerminacion = auxDate;
                    contrato.Estado = "Vigente";
                    contrato.ModifiedBy = View.UserSession.IdUser;
                    contrato.ModifiedOn = DateTime.Now;

                    _contratoService.Modify(contrato);
                    var log = GetLog();
                    log.Descripcion = string.Format("El usuario [{0}] ha agregado fases al contrato.", View.UserSession.Nombres);
                    _log.Add(log);
                }

                View.GoToAdminContrato();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        bool CheckFasesContrato()
        {
            var errorList = new List<string>();

            int totalFasesExploratorio = 0;

            var fasesView = View.FasesContrato;

            foreach (var f in fasesView)
            {
                if (f.FaseId != 0)
                    totalFasesExploratorio += f.DuracionFase;
            }

            if (totalFasesExploratorio != View.MaxTotalDuracionFaseExploratorio)
            {
                errorList.Add(string.Format("La duración total (meses) de las fases [Exploratorio] debe ser ({0}). Total Ingresadas:{1} meses.", View.MaxTotalDuracionFaseExploratorio, totalFasesExploratorio));

                View.AddErrorMessages(errorList);

                return false;
            }

            return true;
        }

        public void CancelContrato()
        {
            try
            {
                _adoService.DeleteContrato(Convert.ToInt32(View.IdContrato));

                View.GoToContratoList();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        LogContratos GetLog()
        {
            var model = new LogContratos();

            model.IdLog = Guid.NewGuid();
            model.IdContrato = Convert.ToInt32(View.IdContrato);
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}