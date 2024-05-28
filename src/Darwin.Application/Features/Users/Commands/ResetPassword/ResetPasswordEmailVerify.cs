using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.Users;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Users.Commands.ResetPassword;

public static class ResetPasswordEmailVerify
{
    public record Command(ResetPasswordRequest Model, string userId, string Token) : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHanler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly IUserService _userService;

        public CommandHanler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _userService.ResetPasswordEmailVerify(request.Model.Password, request.userId, request.Token);

            return DarwinResponse<NoContentDto>.Success();
        }
    }
}
