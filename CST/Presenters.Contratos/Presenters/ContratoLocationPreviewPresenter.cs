using System;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.Presenters
{
    public class ContratoLocationPreviewPresenter : Presenter<IContratoLocationPreviewView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfEmpresasManagementServices _empresasService;
        readonly ISfTiposContratoManagementServices _tipoContratoService;
        readonly ISfBloquesManagementServices _bloquesService;
        readonly IContratosAdoService _adoService;
        readonly ISfLogContratosManagementServices _log;

        public ContratoLocationPreviewPresenter(ISfContratosManagementServices contratoService,
                                                ISfEmpresasManagementServices empresasService,
                                                ISfTiposContratoManagementServices tipoContratoService,
                                                ISfBloquesManagementServices bloquesService,
                                                IContratosAdoService adoService,
                                                ISfLogContratosManagementServices log)
        {
            _empresasService = empresasService;
            _tipoContratoService = tipoContratoService;
            _bloquesService = bloquesService;
            _contratoService = contratoService;
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
        }

        public void LoadInit()
        {            
            LoadEmpresas();
            LoadTipoContratos();            
            LoadContrato();
        }

        void LoadEmpresas()
        {
            try
            {
                var items = _empresasService.FindBySpec(true);
                View.LoadEmpresas(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadTipoContratos()
        {
            try
            {
                var items = _tipoContratoService.FindBySpec(true);
                View.LoadTipoContratos(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadBloques(string idBloque)
        {
            try
            {
                var items = _adoService.GetBloquesSinContratoIncluyeBloque(idBloque);
                View.LoadBloques(items);
                View.IdBloque = idBloque;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadContrato()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(View.IdContrato));
                if (contrato != null)
                {
                    View.NombreContrato = string.Format("{0} - ", contrato.Nombre);
                    View.NumeroContrato = contrato.NumeroContrato;
                    View.DescripcionContrato = contrato.Descripcion;
                    View.EstadoContrato = contrato.Estado;
                    View.Empresa = contrato.Empresas.RazonSocial;
                    View.Bloque = contrato.Bloques.Descripcion;
                    View.TipoContrato = contrato.TiposContrato.Descripcion;
                    View.FechaFirma = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaFirma);
                    View.FechaEfectiva = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaInicio);
                    View.Periodo = string.Format("{0}", UppercaseFirst(string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaTerminacion)));                    
                    View.ImagenContrato = contrato.ImagenContrato;

                    View.IdEmpresa = contrato.IdEmpresa;
                    View.IdTipoContrato = contrato.IdTipoContrato;

                    View.Latitud = contrato.GLatitud;
                    View.Longitud = contrato.GLongitud;

                    LoadGmapMarkers(contrato);

                    LoadBloques(contrato.IdBloque);

                    View.EnableEdit(false);
                    View.CanEdit(contrato.Estado == "Vigente" && View.UserSession.IsInRole("Administrador"));
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadGmapMarkers(Domain.MainModules.Entities.Contratos contrato)
        {
            var marks = new List<Dto_GoogleMapMarker>();
            try
            {
                if (contrato != null)
                {
                    if (contrato.GLatitud.HasValue && contrato.GLongitud.HasValue)
                    {
                        var dtoMark = new Dto_GoogleMapMarker();
                        dtoMark.Name = contrato.Nombre;
                        dtoMark.Description = contrato.Nombre;
                        dtoMark.Latitude = string.Format("{0}", contrato.GLatitud.Value).Replace(',', '.');
                        dtoMark.Longitude = string.Format("{0}", contrato.GLongitud.Value).Replace(',', '.');

                        marks.Add(dtoMark);
                    }
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadGmapMarkers(marks);
        }

        public void SaveContrato()
        {
            try
            {
                var model = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                if (View.NumeroContrato != model.NumeroContrato)
                {
                    if (_contratoService.ExistsContratoByNumero(View.NumeroContrato))
                    {
                        var errorMessages = new List<string>();
                        errorMessages.Add(string.Format("Ya existe un contrato con el numero [{0}]. Por favor ingrese un numero de contrato diferente.", View.NumeroContrato));
                        View.AddErrorMessages(errorMessages);
                        return;
                    }
                }

                model.NumeroContrato = View.NumeroContrato;
                model.Descripcion = View.DescripcionContrato;
                model.IdEmpresa = View.IdEmpresa;
                model.IdBloque = View.IdBloque;
                model.IdTipoContrato = View.IdTipoContrato;
                model.GLatitud = View.Latitud;
                model.GLongitud = View.Longitud;

                if (!string.IsNullOrEmpty(View.ImagenContratoEdit))
                {
                    model.ImagenContrato = View.ImagenContratoEdit;
                    View.SaveImagenContrato(model.IdContrato);
                }
                
                _contratoService.Modify(model);

                var log = GetLog();
                log.IdContrato = model.IdContrato;  
                log.Descripcion = string.Format("El contrato ha sido modificado por [{0}].", View.UserSession.Nombres);

                _log.Add(log);                               

                LoadContrato();
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
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}