using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class SqlHelper
    {
        private readonly ISqlHelper _sql;

        public SqlHelper(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable EjecutarStoredProcedure(string storedProcedure, Dictionary<string, string> parametros)
        {
            try
            {
                var parameters = GetParams(parametros);
                return _sql.ExecuteDataTable(storedProcedure, CommandType.StoredProcedure, parameters);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string EjecutarScalarSp(string storedProcedure, Dictionary<string, string> parametros)
        {
            try
            {
                var parameters = GetParams(parametros);
                var result = _sql.ExecuteScalar(storedProcedure, CommandType.StoredProcedure, parameters);
                return result == null ? string.Empty : result.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static SqlParameter[] GetParams(Dictionary<string, string> parametros)
        {
            var parameter = new SqlParameter[parametros.Count];
            var index = 0;
            foreach (var parametro in parametros)
            {
                parameter[index] = new SqlParameter(string.Format("@{0}", parametro.Key), parametro.Value);
                index++;
            }
            return parameter;
        }
    }
}