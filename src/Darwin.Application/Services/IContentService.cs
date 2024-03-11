using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Contents;
using Darwin.Domain.ResponseModels.Contents;

namespace Darwin.Application.Services;

public interface IContentService
{
    Task<CreatedContentResponse> CreateAsync(CreateContentRequest request);
    Task<UpdatedContentResponse> UpdateAsync(Guid id, UpdateContentRequest request);
    Task DeleteAsync(Guid id);
    Task<List<GetContentResponse>> GetAllAsync();
    Task<GetContentByIdResponse> GetByIdAsync(Guid id);
    Task<GetContentListResponse> GetListAsync(GetPaginationListRequest request);
    Task<List<SearchContentResponse>> SearchAsync(string searchText);
    Task<List<GetContentResponse>> GetNewContentsAsync(int size);
}
