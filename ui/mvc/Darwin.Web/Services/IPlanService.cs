using Darwin.Web.Models.Plans;
using Darwin.Web.ViewModels;

namespace Darwin.Web.Services;

public interface IPlanService
{
    Task<List<PlanViewModel>> GetAllAsync();
    Task<PlanViewModel> GetByIdAsync(Guid id);
    Task CreateAsync(CreatePlanDto createPlanDto);
    Task UpdateAsync(UpdatePlanDto updatePlanDto);
    Task DeleteAsync(Guid id);
}
