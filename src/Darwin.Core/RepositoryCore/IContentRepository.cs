using Darwin.Core.Entities;

namespace Darwin.Core.RepositoryCore;

public interface IContentRepository : IGenericRepository<Content>
{
    IQueryable<Content> GetList();
}
