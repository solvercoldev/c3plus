using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfRadicadosManagementServices : IGenericServices<Radicados>
    {
        List<Radicados> GetByContratoTipoEstadoText(int idContrato, string tipo, string estado, string searchText);
        List<Radicados> GetRadicadosPendienteByContrato(int idContrato);

        Radicados GetCompleteById(long idRadicado);
        Radicados GetById(long idRadicado);
    }
}