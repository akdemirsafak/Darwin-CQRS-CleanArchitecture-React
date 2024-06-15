using System.Linq.Expressions;

namespace Darwin.Contents.Core.AbstractRepositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
    //IQueryable<T> GetList(Expression<Func<T, bool>> filter = null);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> CreateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<IQueryable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);
}
