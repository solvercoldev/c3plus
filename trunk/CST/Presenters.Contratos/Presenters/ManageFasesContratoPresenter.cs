using System;
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
        readonly IContratosAdoService _adoService;
        readonly ISfLogContratosManagementServices _log;

        public ManageFasesContratoPresenter(ISfContratosManagementServices contratoService,
                                 ISfFasesManagementServices fasesService,
                                 ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                 ISfNovedadesFaseManagementServices novedadesFaseService,
                                 IContratosAdoService adoService,
                                 ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _fasesService = fasesService;
            _seccionesServices = seccionesServices;
            _novedadesFaseService = novedadesFaseService;
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
            model.Descripcion = string.Format("El usuario [{0}] ha ingresado una novedad de fase de tipo [{1}]. Comentarios", View.UserSession.Nombres, View.TipoOperacion, View.Descripcion);
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}