using Darwin.Web.Models.Moods;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public interface IMoodService
{
    Task<List<MoodViewModel>> GetAllAsync();
    //Task<MoodViewModel> GetByIdAsync(Guid id);
    Task CreateAsync(CreateMoodDto createMoodDto);
    Task UpdateAsync(UpdateMoodDto updateMoodDto);
    Task DeleteAsync(Guid id);
}
