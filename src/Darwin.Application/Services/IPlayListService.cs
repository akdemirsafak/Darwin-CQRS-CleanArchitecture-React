using Darwin.Domain.RequestModels.PlayLists;
using Darwin.Domain.ResponseModels.PlayLists;

namespace Darwin.Application.Services;

public interface IPlayListService
{
    Task<CreatedPlayListResponse> CreateAsync(CreatePlayListRequest request, string creatorId);
    Task<UpdatedPlayListResponse> UpdateAsync(Guid id, UpdatePlayListRequest request, string creatorId);
    Task DeleteAsync(Guid id);
    Task<List<GetPlayListResponse>> GetAllAsync();
    Task<List<GetPlayListResponse>> GetAllListsOfUserAsync(string currentUserId);

    Task<GetPlayListByIdResponse> GetByIdAsync(Guid id);
    Task<GetPlayListByIdResponse> AddContentsToPlayList(AddContentsToPlayListRequest request);
    Task<GetPlayListByIdResponse> RemoveContentsFromPlayList(RemoveContentsFromPlayListRequest request);
}
