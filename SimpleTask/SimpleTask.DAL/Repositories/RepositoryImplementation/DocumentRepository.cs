using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using SimpleTask.DAL.Data;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.DAL.Repositories.RepositoryImplementation
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}