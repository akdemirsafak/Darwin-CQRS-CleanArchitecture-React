using Darwin.Core.RepositoryCore;
using Darwin.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Darwin.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DarwinDbContext _dbContext;

        public GenericRepository(DarwinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return await (filter == null ?
                    _dbContext.Set<T>().ToListAsync() :
                    _dbContext.Set<T>().Where(filter).AsNoTracking().ToListAsync());
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(T entity)
        {
            var deletedEntity = _dbContext.Remove(entity);
            deletedEntity.State = EntityState.Deleted;

        }

        public async Task UpdateAsync(T entity)
        {
            var updatedEntity = _dbContext.Update(entity);
            updatedEntity.State = EntityState.Modified;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var addedEntity = _dbContext.Add(entity);
            addedEntity.State = EntityState.Added;
            return entity;
        }
    }
}
