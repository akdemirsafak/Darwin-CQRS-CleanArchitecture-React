using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Domain.ResponseModels.Roles;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Persistance.Services;

public sealed class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task CreateAsync(string name)
    {
        var result = await _roleManager.CreateAsync(new AppRole()
        {
            Name=name,
            ConcurrencyStamp=Guid.NewGuid().ToString()
        });
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

    }

    public async Task<List<GetRoleResponse>> GetAllAsync()
    {
        var roles= await _roleManager.Roles.ToListAsync();
        return roles.Adapt<List<GetRoleResponse>>();
    }
}
