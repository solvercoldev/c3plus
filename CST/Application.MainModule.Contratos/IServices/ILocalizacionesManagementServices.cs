using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfLocalizacionesManagementServices : IGenericServices<Localizaciones>
    {
        Localizaciones GetById(string id);
    }
}
    