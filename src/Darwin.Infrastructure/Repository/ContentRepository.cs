using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure.Repository
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(DarwinDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Content> GetList()
        {
            return _dbContext.Contents.AsNoTracking();
        }
    }
}
