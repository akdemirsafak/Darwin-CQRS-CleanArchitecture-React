using Darwin.Core.Entities;

namespace Darwin.Core.RepositoryCore;

public interface IPlayListRepository : IGenericRepository<PlayList>
{
    Task<PlayList> GetPlayListByIdWithContentsAsync(Guid id);
}