using System.Data;
using System.Data.SqlClient;

namespace Infraestructure.Data.Core
{
    public interface ISqlHelper
    {

        string GetConectionString { get; }

        DataTable ExecuteDataTable(string sql, CommandType cmdType);
        DataTable ExecuteDataTable(string sql, CommandType cmdType, params SqlParameter[] parameters);
        object ExecuteScalar(string sql, CommandType cmdType);
        object ExecuteScalar(string sql, CommandType cmdType, params SqlParameter[] parameters);
        void ExecuteNonquery(string sql, CommandType cmdType);
        void ExecuteNonquery(string sql, CommandType cmdType, params SqlParameter[] parameters);
        void ExecuteBulkInsert(string destinationTable, DataTable sourceTable);
    }
}