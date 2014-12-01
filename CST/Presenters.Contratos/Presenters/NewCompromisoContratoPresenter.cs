using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using System.Collections.Generic;

namespace Presenters.Contratos.Presenters
{
    public class NewCompromisoContratoPresenter : Presenter<INewCompromisoContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfCompromisosManagementServices _compromisosService;
        readonly ISfCamposManagementServices _campoService;
        readonly ISfPozosManagementServices _pozoService;
        readonly ISfFasesManagementServices _fasesService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfPagosObligacionesManagementServices _pagosObligacionesService;
        readonly ISfMonedasManagementServices _monedasService;
        readonly ISfTiposPagoObligacionManagementServices _tipoPagoObligacionService;
        readonly ISfTercerosManagementServices _tercerosService;
        readonly ISfManualAnhManagementServices _manualAnhService;
        readonly ISfEntregablesANHCompromisoManagementServices _entregableAnhService;
        readonly ISfLogContratosManagementServices _log;

        public NewCompromisoContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfCompromisosManagementServices compromisosService,
                                                ISfFasesManagementServices fasesService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISfTBL_Admin_OptionListManagementServices optionListService,
                                                ISfCamposManagementServices campoService,
                                                ISfPozosManagementServices pozoService,
                                                ISfPagosObligacionesManagementServices pagosObligacionesService,
                                                ISfMonedasManagementServices monedasService,
                                                ISfTiposPagoObligacionManagementServices tipoPagoObligacionService,
                                                ISfTercerosManagementServices tercerosService,
                                                ISfManualAnhManagementServices manualAnhService,
                                                ISfEntregablesANHCompromisoManagementServices entregableAnhService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _compromisosService = compromisosService;
            _fasesService = fasesService;
            _usuariosService = usuariosService;
            _optionListService = optionListService;
            _campoService = campoService;
            _pozoService = pozoService;
            _pagosObligacionesService = pagosObligacionesService;
            _monedasService = monedasService;
            _tipoPagoObligacionService = tipoPagoObligacionService;
            _tercerosService = tercerosService;
            _manualAnhService = manualAnhService;
            _entregableAnhService = entregableAnhService;
            _log = log;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            InitView();
            LoadInit();            
        }

        public void LoadInit()
        {
            LoadFases();
            LoadResponsables();
            LoadOptionList();
            LoadBloque();
            LoadMonedas();
            LoadTipoPagoObligaciones();
            LoadTerceros();
            LoadManuales();
        }

        void InitView()
        {
            View.FechaCumplimiento = DateTime.Now;
            View.DiasAlarma = 1;
            View.SelectedManualesANH = new List<DTO_ValueKey>();
        }

        void LoadOptionList()
        {
            var tipoCompromisoList = new List<DTO_ValueKey>();
            var importanciaCompromisoList = new List<DTO_ValueKey>();

            try
            {
                var tipoCompromiso = _optionListService.ObtenerOpcionBykeyModuleId("TipoCompromisoContrato", Convert.ToInt32(View.IdModule));
                var importanciaCompromiso = _optionListService.ObtenerOpcionBykeyModuleId("ImportanciaCompromisoContrato", Convert.ToInt32(View.IdModule));

                if (tipoCompromiso != null)
                {
                    var split = tipoCompromiso.Value.Split('|');

                    foreach (var s in split)
                    {
                        tipoCompromisoList.Add(new DTO_ValueKey() { Id = s, Value = s });
                    }
                }

                if (importanciaCompromiso != null)
                {
                    var split = importanciaCompromiso.Value.Split('|');

                    foreach (var s in split)
                    {
                        importanciaCompromisoList.Add(new DTO_ValueKey() { Id = s, Value = s });
                    }
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadTipoCompromiso(tipoCompromisoList);
            View.LoadImportancia(importanciaCompromisoList);
        }

        void LoadFases()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var items = _fasesService.GetFasesByContrato(Convert.ToInt32(View.IdContrato));
                var fasesList = new List<Fases>();
                
                if (items.Any())
                {
                    foreach (var f in items)
                    {
                        if (DateTime.Now <= f.FechaFinalizacion)
                            fasesList.Add(f);
                    }
                }

                View.LoadFases(fasesList);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadMonedas()
        {
            try
            {
                var items = _monedasService.FindBySpec(true);
                View.LoadMonedas(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadTipoPagoObligaciones()
        {
            try
            {
                var items = _tipoPagoObligacionService.FindBySpec(true);
                View.LoadTipoPago(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadTerceros()
        {
            try
            {
                var items = _tercerosService.FindBySpec(true);
                View.LoadEntidades(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadResponsables()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadResponsables(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadManuales()
        {
            try
            {
                var items = _manualAnhService.FindBySpec(true);

                View.LoadManuales(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
      
        public void SaveCompromiso()
        {
            try
            {
                var model = GetModel();
                _compromisosService.Add(model);
                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                switch (View.TipoAsociacion)                
                {
                    case "Entregable":

                        foreach (var entregable in View.SelectedManualesANH)
                        {
                            var entregableANH = new EntregablesANHCompromiso();
                            entregableANH.IdCompromiso = model.IdCompromiso;
                            entregableANH.IdManualAnh = entregable.Id;
                            entregableANH.Estado = (byte)0;

                            _entregableAnhService.Add(entregableANH);
                        }

                        break;
                    case "Pago":
                        AddPagoObligacion(model.IdCompromiso);
                        break;
                }
                
                var log = GetLog();
                log.Descripcion = string.Format("El usuario [{0}], ha ingresado un nuevo compromiso.", View.UserSession.Nombres);
                _log.Add(log);

                contrato.ModifiedBy = View.UserSession.IdUser;
                contrato.ModifiedOn = DateTime.Now;

                _contratoService.Modify(contrato);

                View.GoToCompromisoView(model.IdCompromiso);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddPagoObligacion(long idCompromiso)
        {
            try
            {
                var model = GetPagoModel(idCompromiso);
                _pagosObligacionesService.Add(model);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadBloque()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            var items = new List<DTO_ValueKey>();

            try
            {
                var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(View.IdContrato));

                if (contrato != null)
                {
                    items.Add(new DTO_ValueKey() { Id = contrato.Bloques.IdBloque, Value = contrato.Bloques.Descripcion });
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadBCP(items);
        }

        public void LoadCampos()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            var items = new List<DTO_ValueKey>();

            try
            {
                var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(View.IdContrato));

                var list = _campoService.GetByBloque(contrato.IdBloque);

                if (list.Any())
                {
                    foreach (var l in list)
                    {
                        items.Add(new DTO_ValueKey() { Id = l.IdCampo, Value = l.Descripcion });
                    }                    
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadBCP(items);
        }

        public void LoadPozos()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            var items = new List<DTO_ValueKey>();

            try
            {
                var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(View.IdContrato));

                var list = _pozoService.GetByBloque(contrato.IdBloque);

                if (list.Any())
                {
                    foreach (var l in list)
                    {
                        items.Add(new DTO_ValueKey() { Id = l.IdPozo, Value = l.Descripcion });
                    }
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadBCP(items);
        }

        Compromisos GetModel()
        {
            var model = new Compromisos();
            
            model.IdFase = View.IdFase;
            model.TipoCompromiso = View.TipoCompromiso;
            model.Nombre = View.Nombre;
            model.Descripcion = View.Descripcion;
            model.Importancia = View.Importancia;
            model.FechaCumplimiento = View.FechaCumplimiento;
            model.Estado = "Programado";
            model.NombreResponsable = View.Responsable;
            
            switch (View.AsociadoA)
            {
                case "Campo":
                    model.IdCampo = View.BCP;
                    break;
                case "Pozo":
                    model.IdPozo = View.BCP;
                    break;
            }

            model.DiasAlarma = View.DiasAlarma;
            model.TipoAsociacion = View.TipoAsociacion;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

        PagosObligaciones GetPagoModel(long idCompromiso)
        {
            var model = new PagosObligaciones();

            model.IdCompromiso = idCompromiso;
            model.IdTercero = View.IdTercero;
            model.IdTipoPagoObligacion = View.IdTipoPagoObligacion;
            model.NumeroDocumento = View.NumeroDocumentoPago;
            model.ValorCobertura = View.ValorCoberturaPago;
            model.Valor = View.ValorPago;
            model.IdMoneda = View.IdMoneda;
            model.Estado = "Programado";
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
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}