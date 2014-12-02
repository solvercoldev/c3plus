using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Domain.MainModules.Entities;
using Application.MainModule.SqlServices.IServices;
using System.Collections.Generic;

namespace Presenters.Contratos.Presenters
{
    public class AdminContratoPresenter : Presenter<IAdminContratoView>
    {
        readonly ISfEmpresasManagementServices _empresasService;
        readonly ISfTiposContratoManagementServices _tipoContratoService;
        readonly ISfBloquesManagementServices _bloquesService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfContratosManagementServices _contratoService;
        readonly IContratosAdoService _adoService;
        readonly ISfLogContratosManagementServices _log;

        public AdminContratoPresenter(ISfEmpresasManagementServices empresasService,
                                      ISfTiposContratoManagementServices tipoContratoService,
                                      ISfBloquesManagementServices bloquesService,
                                      ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                      ISfContratosManagementServices contratoService,
                                      IContratosAdoService adoService,
                                      ISfLogContratosManagementServices log)
        {
            _empresasService = empresasService;
            _tipoContratoService = tipoContratoService;
            _bloquesService = bloquesService;
            _usuariosService = usuariosService;
            _contratoService = contratoService;
            _adoService = adoService;
            _log = log;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInit();
            InitView();

            if (!string.IsNullOrEmpty(View.IdContrato))
                LoadContrato();
        }

        public void LoadInit()
        {
            LoadEmpresas();
            LoadTipoContratos();
            LoadBloques();
            LoadUsuarios();
        }

        void InitView()
        {
            View.FechaFirma = DateTime.Now;
            View.FechaEfectiva = DateTime.Now.AddDays(1);
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

        void LoadBloques()
        {
            try
            {
                var items = _adoService.GetBloquesSinContrato();
                View.LoadBloques(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuarios()
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

        public void SaveContrato()
        {
            try
            {
                var model = GetModel();

                if (_contratoService.ExistsContratoByNumero(View.NumeroContrato))
                {
                    var errorMessages = new List<string>();
                    errorMessages.Add(string.Format("Ya existe un contrato con el numero [{0}]. Por favor ingrese un numero de contrato diferente.", View.NumeroContrato));
                    View.AddErrorMessages(errorMessages);
                    return;
                }

                _contratoService.Add(model);
                var log = GetLog();
                log.IdContrato = model.IdContrato;
                log.Descripcion = string.Format("Contrato creado por [{0}].", View.UserSession.Nombres, model.CreateOn);
                
                _log.Add(log);

                View.IdContrato = string.Format("{0}", model.IdContrato);
                View.SaveImagenContrato(model.IdContrato);
                View.GoToAdminFases(model.IdContrato);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateContrato()
        {
            try
            {
                var model = _contratoService.FindById(Convert.ToInt32(View.IdContrato));
                
                model.NumeroContrato = View.NumeroContrato;
                model.Nombre = View.Nombre;
                model.Descripcion = View.Descripcion;
                model.FechaFirma = View.FechaFirma;
                model.FechaInicio = View.FechaEfectiva;
                model.FechaTerminacion = DateTime.Now;
                model.IdEmpresa = View.IdEmpresa;
                model.IdTipoContrato = View.IdTipoContrato;
                model.IdBloque = View.IdBloque;
                model.IdResponsable = View.IdResponsable;
                model.Estado = "Registrado";
                model.ImagenContrato = View.ImagenContrato;
                model.GLongitud = View.Longitud;
                model.GLatitud = View.Latitud;
                model.ModifiedBy = View.UserSession.IdUser;
                model.ModifiedOn = DateTime.Now;

                _contratoService.Modify(model);

                var log = GetLog();
                log.IdContrato = model.IdContrato;
                log.Descripcion = string.Format("Contrato actualizado por [{0}].", View.UserSession.Nombres, model.CreateOn);

                _log.Add(log);

                View.IdContrato = string.Format("{0}", model.IdContrato);
                View.SaveImagenContrato(model.IdContrato);
                View.GoToAdminFases(model.IdContrato);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadContrato()
        {
            if (string.IsNullOrEmpty(View.IdContrato))
                return;
            try
            {
                var model = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                if (model != null)
                {
                    View.NumeroContrato = model.NumeroContrato;
                    View.Nombre = model.Nombre;
                    View.Descripcion = model.Descripcion;
                    View.FechaFirma = model.FechaFirma;
                    View.FechaEfectiva = model.FechaInicio;
                    View.IdEmpresa = model.IdEmpresa;
                    View.IdTipoContrato = model.IdTipoContrato;
                    View.IdBloque = model.IdBloque;
                    View.IdResponsable = model.IdResponsable;
                    model.GLongitud = View.Longitud;
                    model.GLatitud = View.Latitud;
                }

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
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

        Domain.MainModules.Entities.Contratos GetModel()
        {
            var model = new Domain.MainModules.Entities.Contratos();

            model.NumeroContrato = View.NumeroContrato;
            model.Nombre = View.Nombre;
            model.Descripcion = View.Descripcion;
            model.FechaFirma = View.FechaFirma;
            model.FechaInicio = View.FechaEfectiva;
            model.FechaTerminacion = DateTime.Now;
            model.IdEmpresa = View.IdEmpresa;
            model.IdTipoContrato = View.IdTipoContrato;
            model.IdBloque = View.IdBloque;
            model.IdResponsable = View.IdResponsable;
            model.Estado = "Registrado";
            model.ImagenContrato = View.ImagenContrato;
            model.GLongitud = View.Longitud;
            model.GLatitud = View.Latitud;
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
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}