using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfCamposManagementServices : IGenericServices<Campos>
    {
        List<Campos> GetByBloque(string idBloque);
        Campos GetById(string id);
    }
}