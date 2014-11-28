using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfAnexosComentarioRespuestaManagementServices : IGenericServices<AnexosComentarioRespuesta>
    {
        List<AnexosComentarioRespuesta> GetByComentarioId(decimal idComentario);
        AnexosComentarioRespuesta GetById(decimal id);
    }
}
    