using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfComentariosRespuestaManagementServices : IGenericServices<ComentariosRespuesta>
    {
        List<ComentariosRespuesta> GetByContrato(int idContrato);
        ComentariosRespuesta GetById(decimal id);        
        List<ComentariosRespuesta> GetByIdComentarioRelacionado(decimal idComentario);
    }
}