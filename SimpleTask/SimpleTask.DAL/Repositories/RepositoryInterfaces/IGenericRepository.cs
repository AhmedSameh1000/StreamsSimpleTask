using System.Linq.Expressions;

namespace UdemyProject.Contract.RepositoryContracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsTracking(string[] InclueProperties = null);

        Task<IEnumerable<T>> GetAllAsNoTracking(string[] InclueProperties = null);

        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string[] InclueProperties = null);

        Task<IEnumerable<T>> GetAllAsNoTracking(Expression<Func<T, bool>> filter, string[] InclueProperties = null);

        Task<IEnumerable<T>> GetAllAsTracking(Expression<Func<T, bool>> filter, string[] InclueProperties = null);

        Task Add(T entity);

        Task AddRange(List<T> entities);

        void Update(T entity);

        void Remove(T Entity);

        void RemoveRange(IEnumerable<T> Entities);

        Task<int> GetCount();

        Task<bool> SaveChanges();
    }
}