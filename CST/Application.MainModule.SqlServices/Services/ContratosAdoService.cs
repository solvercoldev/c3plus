using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Application.MainModule.SqlServices.IServices;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Services
{
    public class ContratosAdoService : IContratosAdoService
    {
        private readonly ISqlHelper _sql;

        public ContratosAdoService(ISqlHelper sql)
        {
            _sql = sql;
        }

        public void InsertUsuarioCopiaComentario(string idUsuario, string idComentario)
        {
            var sql = string.Format("insert into UsuarioCopiaComentariosRespuesta(IdComentario,IdUsuario) values({0},{1})", idComentario,idUsuario);
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("InsertUsuarioCopiaComentario", ex);
            }
        }

        public DataTable GetCompromisosView()
        {
            var sql = "Vistas_AllCompromisos";

            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_AllCompromisos", ex);
            }
        }

        public DataTable GetCompromisosPendientesView(string usuario)
        {
            var sql = "Vistas_MisCompromisosPendientes";

            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.StoredProcedure, new SqlParameter("@IdUsuario", usuario));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisCompromisosPendientes", ex);
            }
        }

        public DataTable GetRadicadosView()
        {
            var sql = "Vistas_AllRadicados";

            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_AllRadicados", ex);
            }
        }

        public DataTable GetRadicadosPendientesView(int idUsuario)
        {
            var sql = "Vistas_MisRadicadosPendientes";

            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.StoredProcedure, new SqlParameter("@IdUsuario", idUsuario));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisRadicadosPendientes", ex);
            }
        }


        public void ExtenderFase(int idFase, DateTime fechaFin)
        {
            var sql = "ExtenderFase";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure
                                        , new SqlParameter("IdFase", idFase)
                                        , new SqlParameter("FechaFin", fechaFin));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ExtenderFase", ex);
            }
        }

        public void ProrrogarFase(int idFase, DateTime fechaFin)
        {
            var sql = "ProrrogarFase";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure
                                        , new SqlParameter("IdFase", idFase)
                                        , new SqlParameter("FechaFin", fechaFin));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ProrrogarFase", ex);
            }
        }

        public void CorregirFechaFinFase(int idFase, DateTime fechaFin)
        {
            var sql = "CorregirFechaFinFase";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure
                                        , new SqlParameter("IdFase", idFase)
                                        , new SqlParameter("FechaFin", fechaFin));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CorregirFechaFinFase", ex);
            }
        }

        public void UnificarFase(int idFase, DateTime fechaFin)
        {
            var sql = "UnificarFase";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure
                                        , new SqlParameter("IdFase", idFase)
                                        , new SqlParameter("FechaFin", fechaFin));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("UnificarFase", ex);
            }
        }

        public void DeleteContrato(int idContrato)
        {
            var sql = "DeleteContrato";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure, new SqlParameter("IdContrato", idContrato));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("DeleteContrato", ex);
            }
        }

        public DataTable GetBloquesSinContrato()
        {
            var sql = "GetBloquesSinContrato";

            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("GetBloquesSinContrato", ex);
            }
        }

        public DataTable GetBloquesSinContratoIncluyeBloque(string idBloque)
        {
            var sql = "GetBloquesSinContratoIncluyeBloque";

            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.StoredProcedure, new SqlParameter("IdBloque", idBloque));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("GetBloquesSinContratoIncluyeBloque", ex);
            }
        }

        public void SuspenderContrato(int idContrato, DateTime fechaInicio, DateTime fechaFin)
        {
            var sql = "SuspenderContrato";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure,
                                    new SqlParameter("IdContrato", idContrato)
                                    , new SqlParameter("FechaInicio", fechaInicio)
                                    , new SqlParameter("FechaFin", fechaFin));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("SuspenderContrato", ex);
            }
        }

        public void RestitucionManualContrato(int idContrato, DateTime fechaInicio)
        {
            var sql = "RestitucionManualContrato";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure,
                                    new SqlParameter("IdContrato", idContrato)
                                    , new SqlParameter("FechaInicio", fechaInicio));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("RestitucionManualContrato", ex);
            }
        }


        public void RenunciarContrato(int idContrato)
        {
            var sql = "RenunciarContrato";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure,
                                    new SqlParameter("IdContrato", idContrato));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("RenunciarContrato", ex);
            }
        }


        public void ModificarFechaEfectivaContrato(int idContrato, DateTime fechaInicio)
        {
            var sql = "ModificarFechaEfectivaContrato";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure,
                                    new SqlParameter("IdContrato", idContrato)
                                    , new SqlParameter("FechaInicio", fechaInicio));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ModificarFechaEfectivaContrato", ex);
            }
        }


        public void DeleteDocumentoRadicado(long idRadicado)
        {
            var sql = "DeleteDocumentoRadicado";
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.StoredProcedure,
                                    new SqlParameter("IdRadicado", idRadicado));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("DeleteDocumentoRadicado", ex);
            }
        }
    }
}
