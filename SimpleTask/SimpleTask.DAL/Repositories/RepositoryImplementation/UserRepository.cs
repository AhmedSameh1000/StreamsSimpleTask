using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using SimpleTask.DAL.Data;
using SimpleTask.DAL.Domains;

namespace SimpleTask.DAL.Repositories.RepositoryImplementation
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}