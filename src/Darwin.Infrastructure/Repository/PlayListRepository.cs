using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Darwin.Infrastructure.Repository;

public class PlayListRepository : GenericRepository<PlayList>, IPlayListRepository
{
    public PlayListRepository(DarwinDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PlayList> GetPlayListByIdWithContentsAsync(Guid id)
    {
        return await _dbContext.PlayLists.Include(x => x.Contents).SingleOrDefaultAsync(x => x.Id == id);
    }
    public async Task<PlayList> GetPlayListByIdWithContentsAsync(Expression<Func<PlayList, bool>> filter)
    {
        return await _dbContext.PlayLists.Include(x => x.Contents).SingleOrDefaultAsync(filter);
    }
}
