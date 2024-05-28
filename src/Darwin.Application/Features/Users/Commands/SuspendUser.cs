using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Users.Commands
{
    public static class SuspendUser
    {
        public record Command() : ICommand<DarwinResponse<NoContentDto>>;
        public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
        {
            private readonly IUserService _userService;

            public CommandHandler(IUserService userService)
            {
                _userService = userService;
            }


            public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                await _userService.SuspendUserAsync();
                return DarwinResponse<NoContentDto>.Success(204);
            }
        }
    }
}