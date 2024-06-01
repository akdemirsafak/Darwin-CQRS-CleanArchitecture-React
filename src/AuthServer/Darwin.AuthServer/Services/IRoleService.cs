using Darwin.AuthServer.Models.Requests.Roles;
using Darwin.AuthServer.Models.Responses.Roles;
using Darwin.Shared.Dtos;

namespace Darwin.AuthServer.Services;

public interface IRoleService

{
    Task<DarwinResponse<CreatedRoleResponse>> CreateRoleAsync(CreateRoleRequest request);
    Task<DarwinResponse<List<RoleResponse>>> GetRolesAsync();
    Task<DarwinResponse<RoleResponse>> GetRoleByIdAsync(string id);
    Task<DarwinResponse<UpdatedRoleResponse>> UpdateRoleAsync(UpdateRoleRequest request);
    Task<DarwinResponse<NoContentDto>> DeleteRoleAsync(string id);
}
