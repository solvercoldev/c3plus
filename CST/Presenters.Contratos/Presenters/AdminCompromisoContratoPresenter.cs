﻿using System;
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
    public class AdminCompromisoContratoPresenter : Presenter<IAdminCompromisoContratoNew>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfCompromisosManagementServices _compromisosService;
        readonly ISfPagosObligacionesManagementServices _pagosObligacionesService;
        readonly ISfMonedasManagementServices _monedasService;
        readonly ISfTiposPagoObligacionManagementServices _tipoPagoObligacionService;
        readonly ISfTercerosManagementServices _tercerosService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfLogContratosManagementServices _log;

        public AdminCompromisoContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfCompromisosManagementServices compromisosService,
                                                ISfPagosObligacionesManagementServices pagosObligacionesService,
                                                ISfMonedasManagementServices monedasService,
                                                ISfTiposPagoObligacionManagementServices tipoPagoObligacionService,
                                                ISfTercerosManagementServices tercerosService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _compromisosService = compromisosService;
            _pagosObligacionesService = pagosObligacionesService;
            _monedasService = monedasService;
            _tipoPagoObligacionService = tipoPagoObligacionService;
            _tercerosService = tercerosService;
            _usuariosService = usuariosService;
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
            InitView();
            LoadCompromiso();
        }

        void InitView()
        {
            LoadMonedas();
            LoadTipoPagoObligaciones();
            LoadTerceros();
            LoadResponsables();
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

        void LoadCompromiso()
        {
            if (string.IsNullOrEmpty(View.IdCompromiso)) return;

            try
            {
                var compromiso = _compromisosService.GetCompleteById(Convert.ToInt64(View.IdCompromiso));

                if (compromiso != null)
                {
                    View.Nombre = string.Format("{0} - ",compromiso.Nombre);
                    View.Estado = compromiso.Estado;
                    View.Descripcion = compromiso.Descripcion;
                    View.FechaCumplimiento = compromiso.FechaCumplimiento.GetValueOrDefault();
                    View.FechaCumplimientoStr = compromiso.FechaCumplimiento.GetValueOrDefault().ToString("dd/MM/yyyy");
                    View.FechaReprogramacion = compromiso.FechaCumplimiento.GetValueOrDefault();
                    View.Responsable = compromiso.NombreResponsable;
                    View.TipoCompromiso = compromiso.TipoCompromiso;
                    View.Importancia = compromiso.Importancia;
                    View.PeridoFase = compromiso.Fases.Periodo;
                    View.Fase = compromiso.Fases.Nombre;

                    if (!string.IsNullOrEmpty(compromiso.IdCampo))
                        View.BCP = compromiso.Campos.Descripcion;
                    else if (!string.IsNullOrEmpty(compromiso.IdPozo))
                        View.BCP = compromiso.Pozos.Descripcion;
                    else
                        View.BCP = compromiso.Fases.Contratos.Bloques.Descripcion;                    

                    switch (compromiso.TipoAsociacion)
                    {
                        case "Entregable":
                            var manuales = new List<DTO_ValueKey>();
                            if (compromiso.EntregablesANHCompromiso.Any())
                            {
                                foreach (var ent in compromiso.EntregablesANHCompromiso)
                                {
                                    manuales.Add(new DTO_ValueKey() { Id = ent.ManualAnh.IdManualAnh, Value = ent.ManualAnh.Producto });
                                }
                            }
                            View.LoadManuales(manuales);
                            break;
                        case "Pago":

                            View.TipoPago = string.Format("{0}", compromiso.PagosObligaciones.First().TiposPagoObligacion.Descripcion);
                            View.Entidad = string.Format("{0}", compromiso.PagosObligaciones.First().Terceros.Nombre);
                            View.NumeroDocumento = string.Format("{0}", compromiso.PagosObligaciones.First().NumeroDocumento);
                            View.Valor = string.Format("{0:0,0.0} - {1}", compromiso.PagosObligaciones.First().Valor, compromiso.PagosObligaciones.First().Monedas.Nombre);
                            View.ValorCobertura = string.Format("{0:0,0.0} - {1}", compromiso.PagosObligaciones.First().ValorCobertura, compromiso.PagosObligaciones.First().Monedas.Nombre);

                            View.IdTipoPagoObligacion = compromiso.PagosObligaciones.First().IdTipoPagoObligacion;
                            View.IdTercero = compromiso.PagosObligaciones.First().IdTercero;
                            View.IdMoneda = compromiso.PagosObligaciones.First().IdMoneda;
                            View.ValorPago = compromiso.PagosObligaciones.First().Valor;
                            View.ValorCoberturaPago = compromiso.PagosObligaciones.First().ValorCobertura.GetValueOrDefault();

                            break;
                    }

                    View.ShowTipoAsociacion(compromiso.TipoAsociacion);

                    View.EnableEditCompromiso(false);
                    View.EnableActions(compromiso.Estado == "Programado");
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
      
        public void SaveCompromiso()
        {
            if (string.IsNullOrEmpty(View.IdCompromiso)) return;

            try
            {
                var compromiso = _compromisosService.GetById(Convert.ToInt64(View.IdCompromiso));
                
                if (compromiso != null)
                {
                    compromiso.Descripcion = View.Descripcion;

                    _compromisosService.Modify(compromiso);

                    switch (compromiso.TipoAsociacion)
                    {
                        case "Pago":

                            var pago = _pagosObligacionesService.GetPagoByCompromiso(compromiso.IdCompromiso);

                            pago.IdTipoPagoObligacion = View.IdTipoPagoObligacion;
                            pago.NumeroDocumento = View.NumeroDocumento;
                            pago.IdTercero = View.IdTercero;
                            pago.IdMoneda = View.IdMoneda;
                            pago.Valor = View.ValorPago;
                            pago.ValorCobertura = View.ValorCoberturaPago;

                            _pagosObligacionesService.Modify(pago);

                            break;
                    }
                }

                var log = GetLog();
                log.Descripcion = string.Format("El usuario [{0}], ha modificado el compromiso [{1}], asociado a la [{2}]."
                                                , View.UserSession.Nombres
                                                , compromiso.Nombre
                                                ,compromiso.Fases.Nombre);
                _log.Add(log);

                LoadCompromiso();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddNovedadCompromiso()
        {
            if (string.IsNullOrEmpty(View.IdCompromiso)) return;

            try
            {
                var compromiso = _compromisosService.GetById(Convert.ToInt64(View.IdCompromiso));

                if (compromiso != null)
                {
                    compromiso.ModifiedBy = View.UserSession.IdUser;
                    compromiso.ModifiedOn = DateTime.Now;

                    switch (View.TipoOperacion)
                    {
                        case "Anular":
                            compromiso.Estado = "Anulado";
                            break;
                        case "Confirmar":
                            compromiso.Estado = "Realizado";
                            break;
                        case "Reprogramar":
                            compromiso.FechaCumplimiento = View.FechaReprogramacion;
                            break;
                        case "ReAsignar":
                            compromiso.NombreResponsable = View.ResponsableReprogramacion;
                            break;
                    }
                   
                    _compromisosService.Modify(compromiso);

                    var log = GetLog();
                    log.Descripcion = string.Format("El usuario [{0}], ha [{1}] el compromiso [{2}], asociado a la [{3}]."
                                                    , View.UserSession.Nombres
                                                    , View.TipoOperacion
                                                    , compromiso.Nombre
                                                    , compromiso.Fases.Nombre);
                    _log.Add(log);
                }                

                LoadCompromiso();
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