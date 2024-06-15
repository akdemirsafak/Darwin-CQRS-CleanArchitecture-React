using Darwin.Contents.Core.Dtos.Responses.Category;
using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Core.RequestModels.Categories;

namespace Darwin.Contents.Core.AbstractServices;

public interface ICategoryService
{
    Task<CreatedCategoryResponse> CreateAsync(CreateCategoryRequest request, string imageUrl);
    Task<UpdatedCategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request);
    Task DeleteAsync(Guid id);
    Task<List<GetCategoryResponse>> GetAllAsync();
    Task<GetCategoryResponse> GetByIdAsync(Guid id);
    Task<GetCategoryListResponse> GetListAsync(GetPaginationListRequest request);
}
