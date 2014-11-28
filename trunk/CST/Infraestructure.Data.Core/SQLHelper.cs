using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Infraestructure.Data.Core
{
    public class SqlHelper : ISqlHelper
    {
       
        #region Methods

        /// <summary>
        /// Ejecuta una consulta para retornar una tabla como resultado.
        /// </summary>
        /// <param name="sql">SQL Text a ejecutar.</param>
        /// <param name="cmdType">Command Type (SP,Text)</param>
        /// <returns>Resultado en formato DataTable</returns>
        public DataTable ExecuteDataTable(string sql, CommandType cmdType)
        {
            var dt = new DataTable();
           
            try
            {
                using (var oConn = new SqlConnection(GetConnectionString()))
                {
                    oConn.Open();
                    using (DbCommand oCommand = oConn.CreateCommand())
                    {
                        oCommand.CommandText = sql;
                        oCommand.CommandType = cmdType;
                        var oReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                        dt.Load(oReader);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en DAO_AdoNet, Metodo ExecuteDataTable, Error: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Ejecuta una nueva consulta para retornarn un DataTable con parametros definidos por un SP
        /// </summary>
        /// <param name="strSql">consulta o SP Procedure a ejecutar</param>
        /// <param name="cmdType"></param>
        /// <param name="parameters">Lista de parametros a adicionar al DBCommand</param>
        /// <returns>Resultado en formato DataTable</returns>
        public DataTable ExecuteDataTable(string strSql, CommandType cmdType, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
           
            try
            {
                using (var oConn = new SqlConnection(GetConnectionString()))
                {
                    oConn.Open();
                    using (DbCommand oCommand = oConn.CreateCommand())
                    {

                        oCommand.CommandText = strSql;
                        oCommand.CommandType = cmdType;
                        AttachParameters(oCommand, parameters);
                        var oReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                        dt.Load(oReader);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en DAO_AdoNet, Metodo ExecuteDataTable con parametros, Error: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Ejecuta una nueva consulta que retorna un Scalar
        /// </summary>
        /// <param name="sql">SQL Text a ejecutar</param>
        /// <param name="cmdType">Command Type (SP,Text)</param>
        /// <returns>Resultado en formato Scalar</returns>
        public object ExecuteScalar(string sql, CommandType cmdType)
        {
            object result;

            try
            {
                using (var oConn = new SqlConnection(GetConnectionString()))
                {
                    oConn.Open();
                    using (DbCommand oCommand = oConn.CreateCommand())
                    {
                        oCommand.CommandText = sql;
                        oCommand.CommandType = cmdType;
                        result = oCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en DAO_AdoNet, Metodo ExecuteScalar, Error: {0}", ex.Message));
            }
           
            return result;
        }

        /// <summary>
        /// Ejecuta una nueva consulta que retorna un Scalar
        /// </summary>
        /// <param name="sql">SQL Text a ejecutar</param>
        /// <param name="cmdType">Command Type (SP,Text)</param>
        /// <param name="parameters">Lista de parametros a adicionar al DBCommand</param>
        /// <returns>Resultado en formato Scalar</returns>
        public object ExecuteScalar(string sql, CommandType cmdType, params SqlParameter[] parameters)
        {
            object result;
            
            try
            {
                using (var oConn = new SqlConnection(GetConnectionString()))
                {
                    oConn.Open();
                    using (DbCommand oCommand = oConn.CreateCommand())
                    {
                        oCommand.CommandText = sql;
                        oCommand.CommandType = cmdType;
                        AttachParameters(oCommand, parameters);
                        result = oCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en DAO_AdoNet, Metodo ExecuteScalar con parametros, Error: {0}", ex.Message));
            }
           
            return result;
        }

        /// <summary>
        /// Ejecuta una nueva consulta sin retorno de datos.
        /// </summary>
        /// <param name="sql">SQL Text a ejecutar</param>
        /// <param name="cmdType">Command Type (SP,Text)</param>
        public void ExecuteNonquery(string sql, CommandType cmdType)
        {
           
            try
            {
                using (var oConn = new SqlConnection(GetConnectionString()))
                {
                    oConn.Open();
                    using (DbCommand oCommand = oConn.CreateCommand())
                    {
                        oCommand.CommandText = sql;
                        oCommand.CommandType = cmdType;
                        oCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en DAO_AdoNet, Metodo ExecuteNonquery, Error: {0}", ex.Message));
            }
            
        }

        /// <summary>
        /// Ejecuta una nueva consulta sin retorno de datos.
        /// </summary>
        /// <param name="sql">SQL Text a ejecutar</param>
        /// <param name="cmdType">Command Type (SP,Text)</param>
        /// <param name="parameters">Lista de parametros a adicionar al DBCommand</param>
        public void ExecuteNonquery(string sql, CommandType cmdType, params SqlParameter[] parameters)
        {
           
            try
            {
                using (var oConn = new SqlConnection(GetConnectionString()))
                {
                    oConn.Open();
                    using (DbCommand oCommand = oConn.CreateCommand())
                    {
                        oCommand.CommandText = sql;
                        oCommand.CommandType = cmdType;
                        AttachParameters(oCommand, parameters);
                        oCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en DAO_AdoNet, Metodo ExecuteNonquery, Error: {0}", ex.Message));
            }
            
        }

        /// <summary>
        /// Ejecuta una insercion masiva de datos
        /// </summary>
        /// <param name="destinationTable">Tabla de destino</param>
        /// <param name="sourceTable">Fuente de datos</param>
        public void ExecuteBulkInsert(string destinationTable, DataTable sourceTable)
        {
            try
            {
                using (var oConn = new SqlConnection(GetConnectionString()))
                {
                    oConn.Open();
                    using (var bulkCopy = new SqlBulkCopy(GetConnectionString()))
                    {
                        // column mappings
                        bulkCopy.DestinationTableName = destinationTable;
                        bulkCopy.WriteToServer(sourceTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en DAO_AdoNet, Metodo ExecuteBulkInsert, Error: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Adiciona una lisa de parametros a un DBCommand
        /// </summary>
        /// <param name="command">DBCommand</param>
        /// <param name="commandParameters">Parametros a adicionar al DBCommand</param>
        private static void AttachParameters(IDbCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            foreach (var p in commandParameters)
            {
                command.Parameters.Add(p);
            }
        }

        private static  string GetConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("strConectionString");
        }

        public string GetConectionString
        {
            get { return GetConnectionString(); }
        }
        #endregion        
    }
}