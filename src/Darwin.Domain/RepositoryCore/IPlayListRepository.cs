using Darwin.Domain.ResponseModels.PlayLists;

namespace Darwin.Domain.RepositoryCore;

public interface IPlayListRepository
{
    Task<List<GetPlayListResponse>> GetAllAsync();
    Task<GetPlayListByIdResponse> GetByIdAsync(Guid id);
    Task<List<GetPlayListResponse>> GetAllListsOfUserAsync(string creatorId);
}
