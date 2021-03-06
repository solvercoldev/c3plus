using Domain.MainModule.Contratos.Contracts;
using Infraestructure.Data.Core;
using Infrastructure.CrossCutting.Logging;
using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infrastructure.Data.MainModule.Contratos.Repositories
{
    public class DocumentosRadicadoRepository : GenericRepository<DocumentosRadicado>, IDocumentosRadicadoRepository 
    {
        public DocumentosRadicadoRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
    }
}
    