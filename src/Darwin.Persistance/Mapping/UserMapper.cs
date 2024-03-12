using Darwin.Domain.Entities;
using Darwin.Domain.ResponseModels.Users;
using Riok.Mapperly.Abstractions;

namespace Darwin.Persistance.Mapping;

[Mapper]
public partial class UserMapper
{
    public partial GetUserResponse AppUserToGetUserResponse(AppUser user);
    public partial List<GetUserResponse> AppUserListToGetUserResponseList(List<AppUser> userList);
}
