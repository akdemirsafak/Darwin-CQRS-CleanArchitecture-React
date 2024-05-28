using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.Roles;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Roles;

public sealed class GetRoles
{
    public record Query() : IQuery<DarwinResponse<List<GetRoleResponse>>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetRoleResponse>>>
    {
        private readonly IRoleService _roleService;

        public QueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<DarwinResponse<List<GetRoleResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var roles= await _roleService.GetAllAsync();
            return DarwinResponse<List<GetRoleResponse>>.Success(roles);
        }
    }
}
