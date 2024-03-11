using Darwin.Domain.ResponseModels.Roles;

namespace Darwin.Application.Services;

public interface IRoleService
{
    Task CreateAsync(string name);
    Task<List<GetRoleResponse>> GetAllAsync();
}
