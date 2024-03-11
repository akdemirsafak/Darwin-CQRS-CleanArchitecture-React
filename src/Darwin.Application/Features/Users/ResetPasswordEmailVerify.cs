using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using Darwin.Domain.RequestModels.Users;

namespace Darwin.Application.Features.Users;

public static class ResetPasswordEmailVerify
{
    public record Command(ResetPasswordRequest Model, string userId, string Token) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHanler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IUserService _userService;

        public CommandHanler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _userService.ResetPasswordEmailVerify(request.Model.Password, request.userId, request.Token);

            return DarwinResponse<NoContent>.Success();
        }
    }
}
