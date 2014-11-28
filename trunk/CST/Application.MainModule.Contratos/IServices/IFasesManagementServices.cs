using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfFasesManagementServices : IGenericServices<Fases>
    {
        List<Fases> GetFasesByContrato(int idContrato);
    }
}
    