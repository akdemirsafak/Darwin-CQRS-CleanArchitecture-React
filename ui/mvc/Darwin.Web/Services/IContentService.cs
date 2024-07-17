using Darwin.Web.Models.Contents;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public interface IContentService
{
    Task<List<ContentViewModel>> GetAllAsync();
    Task<ContentViewModel> GetByIdAsync(Guid id);
    Task CreateAsync(CreateContentDto createContentDto);
    Task UpdateAsync(UpdateContentDto updateContentDto);
    Task DeleteAsync(Guid id);

}
