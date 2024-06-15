using Darwin.Contents.Core.AbstractRepositories;
using Darwin.Contents.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Darwin.Contents.Repository.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await (filter == null ? _dbSet.ToListAsync() : _dbSet.Where(filter).ToListAsync());
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.Where(filter).FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IQueryable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await Task.FromResult(_dbSet.AsNoTracking());
    }
}
