using Darwin.AuthServer.Entities;
using Darwin.AuthServer.Models.Requests.Roles;
using Darwin.AuthServer.Models.Responses.Roles;
using Darwin.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darwin.AuthServer.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<DarwinResponse<CreatedRoleResponse>> CreateRoleAsync(CreateRoleRequest request)
    {
        var existRole=await _roleManager.FindByNameAsync(request.name);
        if (existRole != null)
            return DarwinResponse<CreatedRoleResponse>.Fail("Role already exist", 400);
        return DarwinResponse<CreatedRoleResponse>.Success(new CreatedRoleResponse(existRole.Id, existRole.Name), 201);
    }

    public async Task<DarwinResponse<NoContentDto>> DeleteRoleAsync(string id)
    {
        var role =await _roleManager.FindByIdAsync(id);
        if (role is null)
            return DarwinResponse<NoContentDto>.Fail("Role not found", 404);

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
            return DarwinResponse<NoContentDto>.Fail(result.Errors.First().Description, 400);

        return DarwinResponse<NoContentDto>.Success(204);
    }

    public async Task<DarwinResponse<RoleResponse>> GetRoleByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role is null)
            return DarwinResponse<RoleResponse>.Fail("Role not found", 404);
        return DarwinResponse<RoleResponse>.Success(new RoleResponse(role.Id, role.Name), 200);
    }

    public async Task<DarwinResponse<List<RoleResponse>>> GetRolesAsync()
    {
        var roles= await _roleManager.Roles.ToListAsync();
        return DarwinResponse<List<RoleResponse>>.Success(roles.Select(x => new RoleResponse(x.Id, x.Name)).ToList(), 200);
    }

    public async Task<DarwinResponse<UpdatedRoleResponse>> UpdateRoleAsync(UpdateRoleRequest request)
    {
        var existUser= await _roleManager.FindByIdAsync(request.Id);
        if (existUser is null)
            return DarwinResponse<UpdatedRoleResponse>.Fail("Role not found", 404);

        existUser.Name = request.Name;
        var result = await _roleManager.UpdateAsync(existUser);
        if (!result.Succeeded)
            return DarwinResponse<UpdatedRoleResponse>.Fail(result.Errors.First().Description, 400);

        return DarwinResponse<UpdatedRoleResponse>.Success(new UpdatedRoleResponse(existUser.Id, existUser.Name), 200);
    }
}
