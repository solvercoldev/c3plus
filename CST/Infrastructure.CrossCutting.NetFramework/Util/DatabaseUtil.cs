using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using Infrastructure.CrossCutting.NetFramework.Enums;
using log4net;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public class DatabaseUtil
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DatabaseUtil));

        private DatabaseUtil()
        {
        }

        /// <summary>
        /// Get the current database type.
        /// </summary>
        /// <returns></returns>
        public static DatabaseType GetCurrentDatabaseType()
        {
           return DatabaseType.MsSql2000;
        }

        private static string  ConnectionString
        {
            get { return ConfigurationManager.AppSettings.Get("strConectionString"); }
        }

        /// <summary>
        /// Execute a given SQL script file.
        /// </summary>
        /// <param name="scriptFilePath"></param>
        public static void ExecuteSqlScript(string scriptFilePath)
        {
            Log.Info("Executing script: " + scriptFilePath);
            var delimiter = GetDelimiter();
            var scriptFileStreamReader = new StreamReader(scriptFilePath);
            var completeScript = scriptFileStreamReader.ReadToEnd();
            IDbConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.Transaction = transaction;
                var splitRegex = delimiter + @"\s*\n";
                var sqlCommands = Regex.Split(completeScript, splitRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                // Strip the delimiter from the last command. It might not be caught by the regex.
                sqlCommands[sqlCommands.Length - 1] =
                    Regex.Replace(sqlCommands[sqlCommands.Length - 1], delimiter, String.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                foreach (var sqlCommand in sqlCommands)
                {
                    if (sqlCommand.Trim().Length > 0)
                    {
                        Log.Info("Executing the follwing command: " + sqlCommand);
                        cmd.CommandText = sqlCommand;
                        cmd.ExecuteNonQuery();
                    }
                }

                Log.Info("Committing transaction for script: " + scriptFilePath);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Log.Warn("Rolling back transaction for script: " + scriptFilePath);
                Log.Error("An error occured while executing the following script: " + scriptFilePath, ex);
                transaction.Rollback();
                throw new Exception("An error occured while executing the following script: " + scriptFilePath, ex);
            }
            finally
            {
                connection.Close();
                scriptFileStreamReader.Close();
            }
        }

        /// <summary>
        /// Get the version of the given assembly from the database.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Version GetAssemblyVersion(string assembly)
        {
            Version version = null;

            IDbConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            // TODO: create proper NHibernate mapping for version :).
            var sql = String.Format("SELECT major, minor, patch FROM TBL_Admin_ModuleVersion WHERE assembly = '{0}'", assembly);
            Log.Info("Version query: " + sql);
            var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    version = new Version(Convert.ToInt32(dr["major"]), Convert.ToInt32(dr["minor"]), Convert.ToInt32(dr["patch"]));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("An error occured while retrieving the version for {0}.", assembly), ex);
            }
            finally
            {
                connection.Close();
            }
            return version;
        }

       
        private static string GetDelimiter()
        {
            switch (GetCurrentDatabaseType())
            {
                case DatabaseType.MsSql2000:
                    return "^go";
                case DatabaseType.PostgreSql:
                case DatabaseType.MySql:
                    return ";";
                default:
                    throw new Exception("Unknown database type.");
            }
        }

        public static bool TestDatabaseConnection()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}