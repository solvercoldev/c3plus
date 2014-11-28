using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfNovedadesContratoManagementServices : IGenericServices<NovedadesContrato>
    {
        List<NovedadesContrato> GetNovedadesByContrato(int idContrato);
    }
}
    