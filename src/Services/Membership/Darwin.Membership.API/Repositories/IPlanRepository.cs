using Darwin.Membership.API.Entities;

namespace Darwin.Membership.API.Repositories;

public interface IPlanRepository
{
    Task<IEnumerable<Plan>> GetAllAsync();
    Task<Plan> GetByIdAsync(Guid id);
    Task<Plan> CreateAsync(Plan entity);
    Task UpdateAsync(Plan entity);
    Task DeleteAsync(Plan entity);
}
