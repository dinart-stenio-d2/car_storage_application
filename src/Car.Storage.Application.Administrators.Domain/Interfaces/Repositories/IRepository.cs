using Car.Storage.Application.Administrators.Domain.Common;
using System.Linq.Expressions;

namespace Car.Storage.Application.Administrators.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        public  Task<T> FindFirstOrDefaultByIdAsync(Expression<Func<T, bool>> predicate);
        public Task<T> FindFirstOrDefaultByIdWithEntitiesRelatedAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<PaginatedList<T>> GetAllAsync(int pageNumber, int pageSize);
        public Task<int> CreateAsync(T entity);
        public Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, T newEntity);
        public Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
