using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Moods;
using Darwin.Domain.ResponseModels.Moods;
using Darwin.Model.Response.Moods;

namespace Darwin.Application.Services;

public interface IMoodService
{
    Task<CreatedMoodResponse> CreateAsync(string name, bool isUsable, string imageUrl);
    Task<UpdatedMoodResponse> UpdateAsync(Guid id, UpdateMoodRequest request);
    Task<List<GetMoodResponse>> GetAllAsync();
    Task<GetMoodListResponse> GetListAsync(GetPaginationListRequest request);
}
