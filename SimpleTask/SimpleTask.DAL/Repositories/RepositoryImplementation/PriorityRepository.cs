using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using SimpleTask.DAL.Data;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.DAL.Repositories.RepositoryImplementation
{
    public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
    {
        public PriorityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}