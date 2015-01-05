using System.Data;
using System;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IContratosAdoService
    {
        void InsertUsuarioCopiaComentario(string idUsuario, string idComentario);
        void DeleteContrato(int idContrato);

        DataTable GetCompromisosView();
        DataTable GetCompromisosPendientesView(int usuario);
        DataTable GetCompromisosToNotify();
        DataTable GetRadicadosView();
        DataTable GetRadicadosToNotify();
        DataTable GetRadicadosPendientesView(int idUsuario);

        DataTable GetBloquesSinContrato();

        DataTable GetBloquesSinContratoIncluyeBloque(string idBloque);

        void ExtenderFase(int idFase, DateTime fechaFin);
        void ProrrogarFase(int idFase, DateTime fechaFin);
        void CorregirFechaFinFase(int idFase, DateTime fechaFin);
        void UnificarFase(int idFase, DateTime fechaFin);

        void SuspenderContrato(int idContrato, DateTime fechaInicio, DateTime fechaFin);
        void RestitucionManualContrato(int idContrato, DateTime fechaInicio);
        void ModificarFechaEfectivaContrato(int idContrato, DateTime fechaInicio);
        void RenunciarContrato(int idContrato);

        void DeleteDocumentoRadicado(long idRadicado);
    }
}