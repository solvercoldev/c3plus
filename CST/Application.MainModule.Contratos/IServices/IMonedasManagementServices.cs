using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfMonedasManagementServices : IGenericServices<Monedas>
    {
        Monedas GetById(string id);
    }
}
    