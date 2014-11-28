using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfEstadosAccionManagementServices : IGenericServices<EstadosAccion>
    {
        EstadosAccion GetByEstado(string estado);
    }
}