using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface INewCompromisoContratoView : IView
    {
        string IdContrato { get; }
        int IdFase { get; set; }
        string TipoCompromiso { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        DateTime FechaCumplimiento { get; set; }
        string Importancia { get; set; }
        string AsociadoA { get; set; }
        string BCP { get; set; }
        string Responsable { get; set; }
        int IdResponsable { get; set; }
        int DiasAlarma { get; set; }
        string TipoAsociacion { get; set; }

        string IdTercero { get; set; }
        string IdTipoPagoObligacion { get; set; }
        string IdMoneda { get; set; }
        string IdMonedaCobertura { get; set; }
        string NumeroDocumentoPago { get; set; }
        decimal ValorPago { get; set; }
        decimal ValorCoberturaPago { get; set; }

        List<DTO_ValueKey> SelectedManualesANH { get; set; }

        void LoadFases(List<Fases> items);
        void LoadResponsables(List<TBL_Admin_Usuarios> items);
        void LoadTipoCompromiso(List<DTO_ValueKey> items);
        void LoadImportancia(List<DTO_ValueKey> items);
        void LoadBCP(List<DTO_ValueKey> items);
        void LoadMonedas(List<Monedas> items);
        void LoadTipoPago(List<TiposPagoObligacion> items);
        void LoadEntidades(List<Terceros> items);
        void LoadManuales(List<ManualAnh> items);

        void GoToContratoView();
        void GoToCompromisoView(long idCompromiso);
    }
}