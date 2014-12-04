using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfPozosManagementServices : IGenericServices<Pozos>
    {
        List<Pozos> GetByBloque(string idBloque);
        Pozos GetById(string id);
    }
}