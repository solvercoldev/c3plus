using System.Collections.Generic;
using System.Data;
using Application.MainModule.ExportExcel.Domain;
using Application.MainModule.ExportExcel.IExportServices;
using Domain.MainModules.Entities;

namespace Application.MainModule.ExportExcel.ExportServices
{
    public class ExportToExcel : IExportToExcel
    {
        #region Properties

        public string TituloHoja { get; set; }

        public Dictionary<string, string> Filtros { get; set; }

        #endregion

        #region Members

        public byte[] Exportar(DataTable dt)
        {
            var export = new ExportDataTable {TituloHoja = TituloHoja, Filtros = Filtros};
            return export.Exportar(dt);
        }


        #endregion


    }
}