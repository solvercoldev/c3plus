using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Data;

namespace Presenters.Contratos.IViews
{
    public interface IAdminContratoView : IView
    {
        string IdContrato { get; set; }
        string NumeroContrato { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        DateTime FechaFirma { get; set; }
        DateTime FechaEfectiva { get; set; }
        string IdEmpresa { get; set; }
        string IdTipoContrato { get; set; }
        string IdBloque { get; set; }        
        int IdResponsable { get; set; }
        int IdEstado { get; set; }
        string ImagenContrato { get; }
        decimal? Longitud { get; set; }
        decimal? Latitud { get; set; }

        void LoadEmpresas(List<Empresas> items);
        void LoadTipoContratos(List<TiposContrato> items);
        void LoadBloques(DataTable items);
        void LoadResponsables(List<TBL_Admin_Usuarios> items);

        void GoToAdminFases(int idContrato);
        void SaveImagenContrato(int idContrato);
        void GoToContratoList();        
    }
}