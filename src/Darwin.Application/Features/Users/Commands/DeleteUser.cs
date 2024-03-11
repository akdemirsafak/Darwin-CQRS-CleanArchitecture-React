using Darwin.Application.Common;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;

namespace Darwin.Application.Features.Users.Commands;

public static class DeleteUser
{
    public record Command() : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;

        public CommandHandler(IUserService userService, ICurrentUser currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(_currentUser.GetUserId);
            return DarwinResponse<NoContent>.Success(204);
        }
    }
}