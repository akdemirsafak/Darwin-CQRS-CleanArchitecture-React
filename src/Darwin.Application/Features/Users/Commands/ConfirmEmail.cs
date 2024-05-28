using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Users.Commands;

public static class ConfirmEmail
{
    public record Command(string userId, string token) : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHandler(IUserService _userService) : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _userService.ConfirmEmailAsync(request.userId, request.token);
            return DarwinResponse<NoContentDto>.Success();
        }
    }
}
