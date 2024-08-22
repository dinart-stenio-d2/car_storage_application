using Car.Storage.Application.Administrators.Data.Repositories.EFContext;
using Car.Storage.Application.Administrators.Domain.Common;
using Car.Storage.Application.Administrators.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Car.Storage.Application.Administrators.Data.Repositories
{
    public class AdministratorsRepository<T> : IRepository<T> where T : class
    {
        private readonly CarStorageContext context;
        private readonly DbSet<T> dbSet;

        public AdministratorsRepository(CarStorageContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public async Task<int> CreateAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = await dbSet.FirstOrDefaultAsync(predicate);

                dbSet.Remove(entity);

                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> FindFirstOrDefaultByIdAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dbSet.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<T> FindFirstOrDefaultByIdWithEntitiesRelatedAsync(Expression<Func<T, bool>> predicate,params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = dbSet;

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return await query.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginatedList<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = dbSet.AsQueryable();

                var count = await query.CountAsync();
                var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                return new PaginatedList<T>(items, count, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, T newEntity)
        {
            try
            {
                var entity = await dbSet.SingleAsync(predicate);
                context.Entry(entity).CurrentValues.SetValues(newEntity);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
