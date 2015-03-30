using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using System.Collections.Generic;

namespace Presenters.Contratos.Presenters
{
    public class GeneralContractListPresenter : Presenter<IGeneralContractListView>
    {
        readonly ISfBloquesManagementServices _bloqueService;
        readonly ISfContratosManagementServices _contratoService;

        public GeneralContractListPresenter(ISfContratosManagementServices contratoService,
                                            ISfBloquesManagementServices bloqueService)
        {
            _contratoService = contratoService;
            _bloqueService = bloqueService;
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
            LoadContratos();
        }

        public void LoadInit()
        {
            LoadBloques();
            LoadEstados();
        }

        void InitView()
        {
            CheckActions();
        }

        void CheckActions()
        {
            View.VisibleNewContract = View.UserSession.IsInRole("Administrador");
        }

        void LoadBloques()
        {
            try
            {
                var items = _bloqueService.FindBySpec(true);
                View.LoadBloques(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadEstados()
        {
            try
            {
                var items = new List<DTO_ValueKey>();

                items.Add(new DTO_ValueKey() { Id = "", Value = "Todos" });
                items.Add(new DTO_ValueKey() { Id = "Vigente", Value = "Vigente" });
                items.Add(new DTO_ValueKey() { Id = "Suspendido", Value = "Suspendido" });
                items.Add(new DTO_ValueKey() { Id = "Terminado", Value = "Terminado" });
                items.Add(new DTO_ValueKey() { Id = "Renunciado", Value = "Renunciado" });
                items.Add(new DTO_ValueKey() { Id = "Anulado", Value = "Anulado" });
                items.Add(new DTO_ValueKey() { Id = "Vencido", Value = "Vencido" });

                View.LoadEstados(items);

                View.Estado = "Vigente";
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadContratos()
        {
            try
            {
                var fechaFilter = View.DateFromStr;

                if (!string.IsNullOrEmpty(fechaFilter))
                {
                    var date = new DateTime();
                    fechaFilter = fechaFilter + "-01";

                    if (DateTime.TryParse(fechaFilter, out date))
                    {
                        date = Convert.ToDateTime(fechaFilter);

                        fechaFilter = date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fechaFilter = string.Empty;
                    }
                }

                var items = _contratoService.GetContratoWithNavsByFilter(View.IdBloque, View.Estado, fechaFilter);

                View.LoadContratos(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}