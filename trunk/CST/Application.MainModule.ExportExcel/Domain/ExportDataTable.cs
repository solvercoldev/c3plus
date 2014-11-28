
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using Infragistics.Documents.Excel;

namespace Application.MainModule.ExportExcel.Domain
{
    public class ExportDataTable
    {

        private Workbook _book;
        Worksheet _worksheet;
        private int _filaIncial = 5;
        public string TituloHoja { get; set; }
        public Dictionary<string, string> Filtros { get; set; }

        public byte[] Exportar(DataTable dt)
        {
            _book = new Workbook();
            if (dt.Rows.Count == 0) return null;

            _worksheet = _book.Worksheets.Add(TituloHoja);
            _book.Worksheets[0].MergedCellsRegions.Clear();

            var credivalores = _book.Worksheets[0].MergedCellsRegions.Add(0, 0, 0, dt.Columns.Count - 1);
            credivalores.Value = "SIAC";
            FormatearTitulo(credivalores);

            var titulo = _book.Worksheets[0].MergedCellsRegions.Add(2, 0, 2, dt.Columns.Count - 1);
            titulo.Value = TituloHoja.ToUpper();
            FormatearTitulo(titulo);

            var filaFiltro = 4;
            foreach (var filtro in Filtros)
            {
                _worksheet.Rows[filaFiltro].Cells[0].Value = filtro.Key.Contains(".") ? filtro.Key.Split('.')[1] : filtro.Key;
                FormatearFiltros(filaFiltro, 0);
                _worksheet.Rows[filaFiltro].Cells[1].Value = filtro.Value;
                FormatearFiltros(filaFiltro, 1);
                filaFiltro++;
            }

            _filaIncial = filaFiltro + 2;
            GenerarExcel(dt);
            var stream = new MemoryStream();
            _book.Save(stream);
            return stream.GetBuffer();
        }

        private void GenerarExcel(DataTable dt)
        {
            var iCell = 0;


            foreach (DataColumn col in dt.Columns)
            {
                var iRow = _filaIncial;
                _worksheet.Rows[_filaIncial - 1].Cells[iCell].Value = col.ColumnName.ToUpper();
                FormatearCelda(_filaIncial - 1, iCell);
                foreach (DataRow r in dt.Rows)
                {

                    if (r[col.ColumnName] is decimal)
                    {
                        _worksheet.Rows[iRow].Cells[iCell].Value = Convert.ToDecimal(r[col.ColumnName]);
                        _worksheet.Rows[iRow].Cells[iCell].CellFormat.FormatString = "#,##0.00_);[Red](#,##0.00)";
                        _worksheet.Rows[iRow].Cells[iCell].CellFormat.Alignment = HorizontalCellAlignment.Right;
                    }
                    else
                    {
                        _worksheet.Rows[iRow].Cells[iCell].Value = r[col.ColumnName];
                        _worksheet.Rows[iRow].Cells[iCell].CellFormat.Alignment = HorizontalCellAlignment.Left;

                    }

                    iRow += 1;
                }
                iCell += 1;
            }
        }

        #region Formatos

        private void FormatearCelda(int fila, int columna)
        {
            _worksheet.Rows[fila].Cells[columna].CellFormat.LeftBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.LeftBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.BottomBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.BottomBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.RightBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.RightBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.TopBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.TopBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.Alignment = HorizontalCellAlignment.Center;
            _worksheet.Rows[fila].Cells[columna].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
        }

        private static void FormatearTitulo(WorksheetMergedCellsRegion merged)
        {
            merged.CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            merged.CellFormat.Font.Height = 450;
            merged.CellFormat.Font.Color = ColorTranslator.FromHtml("#FFFFFF");
            //merged.CellFormat.FillPattern = FillPatternStyle.Solid;
            merged.CellFormat.FillPatternBackgroundColor = ColorTranslator.FromHtml("#FBEAE8");
        }

        private void FormatearFiltros(int fila, int columna)
        {
            _worksheet.Rows[fila].Cells[columna].CellFormat.LeftBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.LeftBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.BottomBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.BottomBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.RightBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.RightBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.TopBorderStyle = CellBorderLineStyle.Default;
            _worksheet.Rows[fila].Cells[columna].CellFormat.TopBorderColor = Color.Black;
            _worksheet.Rows[fila].Cells[columna].CellFormat.Alignment = HorizontalCellAlignment.Center;
            _worksheet.Rows[fila].Cells[columna].CellFormat.Font.Height = 250;
            _worksheet.Rows[fila].Cells[columna].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            _worksheet.Columns[columna].Width = 6000;
        }

        #endregion
    }
}