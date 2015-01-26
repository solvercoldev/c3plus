using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfEntregablesANHCompromisoManagementServices : IGenericServices<EntregablesANHCompromiso>
    {
        List<EntregablesANHCompromiso> GetEntregablesByCompromiso(long idCompromiso);

        EntregablesANHCompromiso GetEntregableByCompromiso(long idCompromiso, string idManual);
    }
}