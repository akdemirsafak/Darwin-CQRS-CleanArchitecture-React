using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Categories;
using Darwin.Domain.ResponseModels.Categories;

namespace Darwin.Application.Services;

public interface ICategoryService
{
    Task<CreatedCategoryResponse> CreateAsync(CreateCategoryRequest request, string imageUrl);
    Task<UpdatedCategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request);
    Task DeleteAsync(Guid id);
    Task<List<GetCategoryResponse>> GetAllAsync();
    Task<GetCategoryResponse> GetByIdAsync(Guid id);
    Task<GetCategoryListResponse> GetListAsync(GetPaginationListRequest request);
}
