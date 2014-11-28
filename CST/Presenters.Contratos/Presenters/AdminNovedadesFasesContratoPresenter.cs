using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.Presenters
{
    public class AdminNovedadesFasesContratoPresenter : Presenter<IAdminNovedadesFasesContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfNovedadesFaseManagementServices _novedadesFaseService;
        readonly ISfFasesManagementServices _fasesService;

        public AdminNovedadesFasesContratoPresenter(ISfContratosManagementServices contratoService,
                                                    ISfNovedadesFaseManagementServices novedadesFaseoService,
                                                    ISfFasesManagementServices fasesService)
        {
            _contratoService = contratoService;
            _novedadesFaseService = novedadesFaseoService;
            _fasesService = fasesService;
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
            LoadFases();
            LoadNovedades();
        }

        void InitView()
        {
            
        }

        void LoadFases()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var items = _fasesService.GetFasesByContrato(Convert.ToInt32(View.IdContrato));
                View.LoadFases(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadNovedades()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;
            try
            {
                var items = _novedadesFaseService.GetByContratoFase(Convert.ToInt32(View.IdContrato), View.IdFase);

                if (items.Any())
                {
                    items = items.OrderBy(x => x.IdFase).ThenBy(x => x.CreateOn).ToList();
                }

                View.LoadNovedades(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }              
    }
}