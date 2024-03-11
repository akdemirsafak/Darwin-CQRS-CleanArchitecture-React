using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;

namespace Darwin.Application.Features.Users.Commands;

public static class ConfirmEmail
{
    public record Command(string userId, string token) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler(IUserService _userService) : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _userService.ConfirmEmailAsync(request.userId, request.token);
            return DarwinResponse<NoContent>.Success();
        }
    }
}
