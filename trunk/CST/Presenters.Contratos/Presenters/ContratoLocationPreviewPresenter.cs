using System;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;

namespace Presenters.Contratos.Presenters
{
    public class ContratoLocationPreviewPresenter : Presenter<IContratoLocationPreviewView>
    {
        readonly ISfContratosManagementServices _contratoService;

        public ContratoLocationPreviewPresenter(ISfContratosManagementServices contratoService)
        {
            _contratoService = contratoService;
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
            LoadContrato();
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
                    View.Bloque = contrato.Bloques.Descripcion;
                    View.TipoContrato = contrato.TiposContrato.Descripcion;
                    View.FechaFirma = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaFirma);
                    View.FechaEfectiva = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaInicio);
                    View.Periodo = string.Format("{0}", UppercaseFirst(string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaTerminacion)));                    
                    View.ImagenContrato = contrato.ImagenContrato;

                    LoadGmapMarkers(contrato);
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
                        dtoMark.Description = contrato.Descripcion;
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
    }
}