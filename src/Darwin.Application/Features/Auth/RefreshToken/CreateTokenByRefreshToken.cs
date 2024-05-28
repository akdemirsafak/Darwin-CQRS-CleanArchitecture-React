using Darwin.Application.Common;
using Darwin.Application.TokenOperations;
using Darwin.Domain.Dtos;
using Darwin.Domain.RequestModels.Authentications;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.Auth.RefreshToken;

public static class CreateTokenByRefreshToken
{
    public record Command(RefreshTokenRequest Model) : ICommand<DarwinResponse<TokenResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly ITokenService _tokenService;

        public CommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var tokenResponse= await _tokenService.CreateTokenByRefreshToken(request.Model.RefreshToken, request.Model.AccessToken);

            return DarwinResponse<TokenResponse>.Success(tokenResponse, 200);
        }
    }

}
