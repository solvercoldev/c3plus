using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IComentariosRespuestaRepository : IRepository<ComentariosRespuesta>
    {
        ComentariosRespuesta GetCompleteEntityBySpec(ISpecification<ComentariosRespuesta> specification);
        List<ComentariosRespuesta> GetCompleteListBySpec(ISpecification<ComentariosRespuesta> specification);
        ComentariosRespuesta GetComentarioById(decimal id);
    }
}