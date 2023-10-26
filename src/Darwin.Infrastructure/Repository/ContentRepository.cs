using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure.Repository;

public class ContentRepository : GenericRepository<Content>, IContentRepository
{
    public ContentRepository(DarwinDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Content> GetContentWithMoodsAndCategoriesAsync(Guid id)
    {
        return await _dbContext.Contents.Include(x => x.Moods).Include(x => x.Categories).SingleOrDefaultAsync(x => x.Id == id);
    }
}
