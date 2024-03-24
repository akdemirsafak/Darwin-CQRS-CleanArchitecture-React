using Darwin.Domain.ResponseModels.Categories;

namespace Darwin.Domain.RepositoryCore;

public interface ICategoryRepository
{
    Task<GetCategoryResponse> GetByIdAsync(Guid id);
    Task<List<GetCategoryResponse>> GetAllAsync();
    Task<List<GetCategoryResponse>> GetListAsync(int offset, int pageSize);
    Task<int> GetTotalRowCountAsync();
}
