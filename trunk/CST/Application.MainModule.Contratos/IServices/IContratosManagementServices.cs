using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;
using System;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfContratosManagementServices : IGenericServices<Domain.MainModules.Entities.Contratos>
    {
        Domain.MainModules.Entities.Contratos GetContratoWithNavsById(int idContrato);

        List<Domain.MainModules.Entities.Contratos> GetContratoWithNavsByFilter(string idEmpresa, DateTime fechaInicioFirma, DateTime fechaFirmaFin);
        List<Domain.MainModules.Entities.Contratos> GetContratoWithNavsByFilter(string idBloque, string estado, string fechaInicio);

        bool ExistsContratoByNumero(string numero);
    }
}
    