using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfCompromisosManagementServices : IGenericServices<Compromisos>
    {
        List<Compromisos> GetByContrato(int idContrato);
        List<Compromisos> GetByContratoFase(int idContrato, int idFase);

        Compromisos GetCompleteById(long idCompromiso);
        Compromisos GetById(long idCompromiso);
    }
}
    