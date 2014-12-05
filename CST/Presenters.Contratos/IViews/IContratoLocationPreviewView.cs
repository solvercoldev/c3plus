using System.Collections.Generic;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Domain.MainModules.Entities;
using System.Data;

namespace Presenters.Contratos.IViews
{
    public interface IContratoLocationPreviewView : IView
    {
        string IdContrato { get; }

        string NombreContrato { get; set; }
        string NumeroContrato { get; set; }
        string EstadoContrato { get; set; }
        string Empresa { get; set; }
        string Bloque { get; set; }
        string TipoContrato { get; set; }
        string FechaFirma { get; set; }
        string FechaEfectiva { get; set; }
        string Periodo { get; set; }
        string ImagenContrato { set; }

        string IdEmpresa { get; set; }
        string IdTipoContrato { get; set; }
        string IdBloque { get; set; }
        string ImagenContratoEdit { get; }
        decimal? Longitud { get; set; }
        decimal? Latitud { get; set; }

        void LoadEmpresas(List<Empresas> items);
        void LoadTipoContratos(List<TiposContrato> items);
        void LoadBloques(DataTable items);

        void SaveImagenContrato(int idContrato);

        void LoadGmapMarkers(List<Dto_GoogleMapMarker> items);

        void CanEdit(bool enable);
        void EnableEdit(bool enable);
        void AddErrorMessages(List<string> errors);
    }
}
