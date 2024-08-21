using System.Linq.Expressions;

namespace Car.Storage.Application.Administrators.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindFirstOrDefaultByIdAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindFirstOrDefaultByIdWithEntitiesRelatedAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, T newEntity);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
