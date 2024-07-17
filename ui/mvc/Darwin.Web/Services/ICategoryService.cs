using Darwin.Web.Models.Categories;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public interface ICategoryService
{
    Task<List<CategoryViewModel>> GetAllAsync();
    Task<CategoryViewModel> GetByIdAsync(Guid id);
    Task CreateAsync(CreateCategoryDto createCategoryDto);
    Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteAsync(Guid id);
}
