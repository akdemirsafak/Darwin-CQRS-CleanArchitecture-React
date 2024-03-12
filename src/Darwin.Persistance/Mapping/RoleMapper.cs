using Darwin.Domain.Entities;
using Darwin.Domain.ResponseModels.Roles;
using Riok.Mapperly.Abstractions;

namespace Darwin.Persistance.Mapping;

[Mapper]
public partial class RoleMapper
{
    public partial List<GetRoleResponse> RoleListToGetRoleResponseList(List<AppRole> roleList);
}
