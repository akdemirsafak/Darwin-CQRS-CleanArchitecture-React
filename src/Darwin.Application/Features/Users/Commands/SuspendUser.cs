using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;

namespace Darwin.Application.Features.Users.Commands
{
    public static class SuspendUser
    {
        public record Command() : ICommand<DarwinResponse<NoContent>>;
        public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
        {
            private readonly IUserService _userService;

            public CommandHandler(IUserService userService)
            {
                _userService = userService;
            }


            public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
            {
                await _userService.SuspendUserAsync();
                return DarwinResponse<NoContent>.Success(204);
            }
        }
    }
}