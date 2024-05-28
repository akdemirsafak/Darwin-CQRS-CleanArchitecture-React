using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.ResponseModels.Users;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Users.Queries;

public static class GetUser
{
    public record Query(string userId) : IQuery<DarwinResponse<GetUserResponse>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetUserResponse>>
    {
        private readonly IUserService _userService;

        public QueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DarwinResponse<GetUserResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user= await _userService.GetByIdAsync(request.userId);
            return DarwinResponse<GetUserResponse>.Success(user);
        }
    }
}