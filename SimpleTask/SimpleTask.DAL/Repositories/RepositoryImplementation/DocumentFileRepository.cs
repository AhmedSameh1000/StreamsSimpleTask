using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using SimpleTask.DAL.Data;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.DAL.Repositories.RepositoryImplementation
{
    public class DocumentFileRepository : GenericRepository<DocumentFile>, IDocumentFileRepository
    {
        public DocumentFileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

}