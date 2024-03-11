using Darwin.Domain.ResponseModels.Categories;

namespace Darwin.Domain.RepositoryCore;

public interface ICategoryRepository
{
    Task<GetCategoryResponse> GetByIdAsync(Guid id);
}
