using Darwin.AuthServer.Models.Requests.Roles;
using Darwin.AuthServer.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.AuthServer.Controllers;


public class RoleController : CustomBaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        var result = await _roleService.CreateRoleAsync(request);
        return CreateActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _roleService.GetRolesAsync();
        return CreateActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(string id)
    {
        var result = await _roleService.GetRoleByIdAsync(id);
        return CreateActionResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest request)
    {
        var result = await _roleService.UpdateRoleAsync(request);
        return CreateActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        return CreateActionResult(result);
    }
}
