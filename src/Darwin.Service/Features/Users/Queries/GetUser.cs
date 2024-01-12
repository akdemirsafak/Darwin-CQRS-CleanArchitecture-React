using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Response.Users;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users.Queries;

public static class GetUser
{
    public record Query() : IQuery<DarwinResponse<GetUserResponse>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetUserResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrentUser _currentUser;

        public QueryHandler(UserManager<AppUser> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }
        public async Task<DarwinResponse<GetUserResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUser.GetUserId);
            if (user is null)
                return DarwinResponse<GetUserResponse>.Fail("User not found.");

            return DarwinResponse<GetUserResponse>.Success(user.Adapt<GetUserResponse>());
        }
    }
}