using Darwin.Contents.Core.Dtos.Responses.Content;
using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Core.RequestModels.Contents;

namespace Darwin.Contents.Core.AbstractServices;
public interface IContentService
{
    Task<CreatedContentResponse> CreateAsync(CreateContentRequest request, string imageUrl);
    Task<UpdatedContentResponse> UpdateAsync(Guid id, UpdateContentRequest request);
    Task DeleteAsync(Guid id);
    Task<List<GetContentResponse>> GetAllAsync();
    Task<GetContentByIdResponse> GetByIdAsync(Guid id);
    Task<GetContentListResponse> GetListAsync(GetPaginationListRequest request);
    Task<List<SearchContentResponse>> SearchAsync(string searchText);
    Task<List<GetContentResponse>> GetNewContentsAsync(int size);
}
