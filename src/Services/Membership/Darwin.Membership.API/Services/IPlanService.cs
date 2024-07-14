using Darwin.Membership.API.Models.Plan;
using Darwin.Shared.Dtos;

namespace Darwin.Membership.API.Services;

public interface IPlanService
{
    Task<DarwinResponse<List<GetPlanResponse>>> GetAllAsync();
    Task<DarwinResponse<GetPlanResponse>> GetByIdAsync(Guid id);
    Task<DarwinResponse<GetPlanResponse>> CreateAsync(CreatePlanRequest request);
    Task<DarwinResponse<GetPlanResponse>> UpdateAsync(Guid id, UpdatePlanRequest request);
    Task<DarwinResponse<bool>> DeleteAsync(Guid id);
}
