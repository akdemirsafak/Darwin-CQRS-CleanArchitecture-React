using Darwin.Core.UnitofWorkCore;
using Darwin.Infrastructure.DbContexts;

namespace Darwin.Service.Uof;

public class UnitOfWork : IUnitOfWork
{
    private readonly DarwinDbContext _dbContext;

    public UnitOfWork(DarwinDbContext dbContext) => _dbContext = dbContext;

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

}
