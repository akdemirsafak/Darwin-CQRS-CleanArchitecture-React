using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;

namespace Darwin.Application.Features.Auth.RevokeAllTokens
{
    public static class RevokeAllTokens
    {
        public record Command() : ICommand<DarwinResponse<NoContent>>;
        public class CommandHandler(IAuthService _authService) : ICommandHandler<Command, DarwinResponse<NoContent>>
        {
            public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
            {
                await _authService.RevokeTokensAsync();
                return DarwinResponse<NoContent>.Success();
            }
        }
    }
}