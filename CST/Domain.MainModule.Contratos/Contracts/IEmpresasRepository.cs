using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IEmpresasRepository : IRepository<Empresas>
    {
        Empresas GetCompleteEntity(ISpecification<Empresas> specification);
    }
}
    