using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Auth.RevokeAllTokens;

public static class RevokeAllTokens
{
    public record Command() : ICommand<DarwinResponse<NoContentDto>>;
    public class CommandHandler(IAuthService _authService) : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _authService.RevokeTokensAsync();
            return DarwinResponse<NoContentDto>.Success();
        }
    }
}