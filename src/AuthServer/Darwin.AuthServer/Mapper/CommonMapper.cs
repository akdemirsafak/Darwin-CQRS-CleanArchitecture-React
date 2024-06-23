using Darwin.AuthServer.Entities;
using Darwin.AuthServer.Models.Responses.Users;
using Riok.Mapperly.Abstractions;

namespace Darwin.AuthServer.Mapper;

[Mapper]
public partial class CommonMapper
{
    public partial UpdatedUserResponse AppUserToUpdatedUserResponse(AppUser appUser);
    public partial GetUserResponse AppUserToGetUserResponse(AppUser appUser);
    public partial List<GetUserResponse> AppUserListToGetUserResponseList(List<AppUser> appUsers);
}
