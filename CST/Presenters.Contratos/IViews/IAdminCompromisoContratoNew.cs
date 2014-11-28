using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminCompromisoContratoNew : IView
    {
        string IdContrato { get; }
        string IdCompromiso { get; }

        string Nombre { get; set; }
        string Estado { get; set; }
        string Descripcion { get; set; }
        DateTime FechaCumplimiento { get; set; }
        string FechaCumplimientoStr { get; set; }
        string Responsable { get; set; }
        string TipoCompromiso { get; set; }
        string Importancia { get; set; }
        string PeridoFase { get; set; }
        string Fase { get; set; }
        string BCP { get; set; }

        string TipoPago { get; set; }
        string Entidad { get; set; }
        string NumeroDocumento { get; set; }
        string Valor { get; set; }
        string ValorCobertura { get; set; }

        string IdTercero { get; set; }
        string IdTipoPagoObligacion { get; set; }        
        string IdMoneda { get; set; }
        decimal ValorPago { get; set; }
        decimal ValorCoberturaPago { get; set; }

        string TipoOperacion { get; set; }

        string ResponsableReprogramacion { get; set; }
        DateTime FechaReprogramacion { get; set; }

        void LoadManuales(List<DTO_ValueKey> items);
        void LoadMonedas(List<Monedas> items);
        void LoadTipoPago(List<TiposPagoObligacion> items);
        void LoadEntidades(List<Terceros> items);
        void LoadResponsables(List<TBL_Admin_Usuarios> items);
        void ShowTipoAsociacion(string tipo);

        void EnableEditCompromiso(bool enable);

        void EnableActions(bool enable);
    }
}