using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class Reclamos
    {
        private readonly ISqlHelper _sql;

        public Reclamos(ISqlHelper sql)
        {
            _sql = sql;
        }

        public string EstadoReclamo(string id)
        {
            try
            {
                const string strSql = " Select Est.Estado from TBL_ModuloReclamos_Reclamo Rec INNER JOIN TBL_Admin_EstadosProceso Est On Rec.IdEstado = Est.IdEstado where Rec.IdContrato = @IdContrato";
                var result = _sql.ExecuteScalar(strSql, CommandType.Text,
                                       new SqlParameter("@IdContrato", id));

                return result == null ? string.Empty : result.ToString();
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("EstadoPedido", ex);
            }
        }

        public string EstadoAccionesPc(string id)
        {
            try
            {
                const string strSql = " Select Est.Estado from TBL_ModuloAPC_Solicitud Sol INNER JOIN TBL_Admin_EstadosProceso Est On Sol.IdEstado = Est.IdEstado where Sol.IdSolucitudAPC = @Id";
                var result = _sql.ExecuteScalar(strSql, CommandType.Text,
                                       new SqlParameter("@Id", id));

                return result == null ? string.Empty : result.ToString();
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("EstadoAccionesPc", ex);
            }
        }

        public DataTable GetReclamoWorkFlowById(string IdContrato)
        {
            try
            {
                return _sql.ExecuteDataTable("GetReclamoWorkFlowById", CommandType.StoredProcedure,
                                       new SqlParameter("@IdContrato", IdContrato));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CargarPedidoCompletoPorIdPedido", ex);
            }
        }

        public DataTable GetAccionesWorkFlowById(string id)
        {
            try
            {
                return _sql.ExecuteDataTable("GetAccionesPcWorkFlowById", CommandType.StoredProcedure,
                                       new SqlParameter("@Id", id));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("GetAccionesWorkFlowById", ex);
            }
        }

        public DataTable ResumenReclamosPanelWorkFlow(string IdContrato)
        {
            try
            {
                return _sql.ExecuteDataTable("GetResumenReclamosPanelWf", CommandType.StoredProcedure,
                                       new SqlParameter("@IdContrato", IdContrato));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ResumenReclamosPanelWorkFlow", ex);
            }
        }
    }
}