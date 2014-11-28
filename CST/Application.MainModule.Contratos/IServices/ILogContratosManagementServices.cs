using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfLogContratosManagementServices : IGenericServices<LogContratos>
    {
        List<LogContratos> GetByIdContrato(int idContrato);
    }
}