using System;
using System.Collections.Generic;
using System.Data;
using Domain.MainModules.Entities;

namespace Application.MainModule.ExportExcel.IExportServices
{
    public interface IExportarDataTableExcel
    {
        string TituloHoja { get; set; }
        Dictionary<string, string> Filtros { get; set; }
        byte[] Exportar(DataTable dt);
    }
}