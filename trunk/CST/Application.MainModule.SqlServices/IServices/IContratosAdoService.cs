using System.Data;
using System;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IContratosAdoService
    {
        void InsertUsuarioCopiaComentario(string idUsuario, string idComentario);
        void DeleteContrato(int idContrato);

        DataTable GetCompromisosView();

        DataTable GetBloquesSinContrato();

        void ExtenderFase(int idFase, DateTime fechaFin);
        void ProrrogarFase(int idFase, DateTime fechaFin);
        void CorregirFechaFinFase(int idFase, DateTime fechaFin);
        void UnificarFase(int idFase, DateTime fechaFin);
    }
}