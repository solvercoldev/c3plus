using System;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Infrastructure.CrossCutting.IoC;

namespace Presenters.Contratos.Presenters
{
    public class ContratoPresenter : Presenter<IContratoView>
    {
        ISfContratosManagementServices _contratoService;
        ISfFasesManagementServices _fasesService;
        readonly ISfTBL_Admin_SeccionesManagementServices _seccionesServices;
        readonly ISfEstadosAccionManagementServices _estadosAccionService;

        public ContratoPresenter(ISfContratosManagementServices contratoService,
                                 ISfFasesManagementServices fasesService,
                                 ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                 ISfEstadosAccionManagementServices estadosAccionService)
        {
            _contratoService = contratoService;
            _fasesService = fasesService;
            _seccionesServices = seccionesServices;
            _estadosAccionService = estadosAccionService;
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
            LoadFases();
            CargarSecciones();
        }

        void InitView()
        {
        }

        public void LoadContrato()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                _contratoService = IoC.Resolve<ISfContratosManagementServices>();
                var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(View.IdContrato));
                if (contrato != null)
                {
                    View.NombreContrato = contrato.Nombre;
                    View.NumeroContrato = contrato.NumeroContrato;
                    View.EstadoContrato = string.Format(" {0}", contrato.Estado);
                    View.Empresa = contrato.Empresas.RazonSocial;
                    View.TipoContrato = contrato.TiposContrato.Descripcion;
                    View.Bloque = contrato.Bloques.Descripcion;
                    View.FechaFirma = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaFirma);
                    View.FechaEfectiva = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaInicio);
                    View.Periodo = string.Format("{0}", UppercaseFirst(string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaTerminacion)));
                    View.ImagenContrato = contrato.ImagenContrato;

                    var estadoAccion = _estadosAccionService.GetByEstado(contrato.Estado);

                    if (estadoAccion != null)
                        View.CanTrabajarFases = estadoAccion.TrabajarFases;

                    View.MsgLogInfo = string.Format("Creado por {0} en {1:dd/MM/yyyy hh:mm tt}. Modificado por {2} en {3:dd/MM/yyyy hh:mm tt}.",
                                                    contrato.TBL_Admin_Usuarios.Nombres, contrato.CreateOn,
                                                    contrato.TBL_Admin_Usuarios1.Nombres, contrato.ModifiedOn);
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadFases()
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

        void CargarSecciones()
        {
            try
            {
                var secciones = _seccionesServices.ListadoSeccionesPorModulo(Convert.ToInt32(View.IdModule));
                View.LoadSecciones(secciones);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}