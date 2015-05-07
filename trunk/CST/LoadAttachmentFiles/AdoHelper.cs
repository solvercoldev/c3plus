using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infraestructure.Data.Core;
using System.Data;
using System.Data.SqlClient;

namespace LoadAttachmentFiles
{
    public class AdoHelper
    {
        readonly SqlHelper _sql;

        public AdoHelper()
        {
            _sql = new SqlHelper();
        }

        public DataTable GetInfoContratoByIdContratoMig(int idContrato)
        {
            var sql = " select	ctr.* " + 
                      " from	Contratos ctr " +
                      " join [C3+]..Contratos ctrMig " +
                      " on ctr.NumeroContrato = ctrMig.NumeroContrato " +
                      " where	ctrMig.IdContrato = @IdContrato ";
            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.Text, new SqlParameter("@IdContrato", idContrato));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetInfoRadicado(int idContrato, int tipoRad, string nombreRad)
        {
            var sql = " select	rad.* " +
                      " from	Contratos ctr " +
                      " join [C3+]..Contratos ctrMig " +
                      " on ctr.NumeroContrato = ctrMig.NumeroContrato " +
                      " join Radicados rad " +
                      " on rad.IdContrato = ctr.IdContrato " +
                      " join [C3+]..Radicados radMig " +
                      " on rad.Numero = radMig.Numero " +
                      " and radMig.IdContrato = ctrMig.IdContrato " +
                      " join [C3+]..DocumentosRadicado docRad " +
                      " on radMig.IdRadicado = docRad.IdRadicado " +
                      " where	rad.IdContrato = @IdContrato " +
                      " and radMig.TipoRadicado = @TipoRad " +
                      " and docRad.Nombre = @NombreRad ";
            try
            {
                return _sql.ExecuteDataTable(sql, CommandType.Text
                    , new SqlParameter("@IdContrato", idContrato)
                    , new SqlParameter("@TipoRad", tipoRad)
                    , new SqlParameter("@NombreRad", nombreRad));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}