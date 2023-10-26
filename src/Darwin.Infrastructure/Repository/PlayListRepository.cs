using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure.Repository;

public class PlayListRepository : GenericRepository<PlayList>, IPlayListRepository
{
    public PlayListRepository(DarwinDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PlayList> GetPlayListByIdWithMusicsAsync(Guid id)
    {
        return await _dbContext.PlayLists.Include(x => x.Musics).SingleOrDefaultAsync(x => x.Id == id);
    }
}
