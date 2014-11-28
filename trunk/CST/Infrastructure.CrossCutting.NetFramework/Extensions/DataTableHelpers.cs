using System;
using System.Data;

namespace Infrastructure.CrossCutting.NetFramework.Extensions
{
    public static class DataTableHelpers
    {

        public static Boolean ExistsColumn(this DataRow dr, String columnName)
        {
            return dr.Table.Columns.Contains(columnName);
        }

        public static String GetValueColumn(this DataRow dr, String columnName)
        {
            return dr[columnName].ToString();
        }
    }
}