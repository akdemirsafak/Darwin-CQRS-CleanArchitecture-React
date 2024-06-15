using Darwin.Contents.Core.Dtos.Responses.Mood;
using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Core.RequestModels.Moods;

namespace Darwin.Contents.Core.AbstractServices;
public interface IMoodService
{
    Task<CreatedMoodResponse> CreateAsync(string name, string imageUrl);
    Task<UpdatedMoodResponse> UpdateAsync(Guid id, UpdateMoodRequest request);
    Task<List<GetMoodResponse>> GetAllAsync();
    Task<GetMoodListResponse> GetListAsync(GetPaginationListRequest request);
}
