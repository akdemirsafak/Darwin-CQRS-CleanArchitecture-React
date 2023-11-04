using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Response.Roles;
using Darwin.Service.Common;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Service.Features.Roles;

public sealed class GetRoles
{
    public record Query() : IQuery<DarwinResponse<List<GetRoleResponse>>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetRoleResponse>>>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public QueryHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<DarwinResponse<List<GetRoleResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var roles= await _roleManager.Roles.ToListAsync(cancellationToken);
            return DarwinResponse<List<GetRoleResponse>>.Success(roles.Adapt<List<GetRoleResponse>>());
        }
    }
}
