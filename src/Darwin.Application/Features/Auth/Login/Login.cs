using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Dtos;
using Darwin.Domain.RequestModels.Users;

namespace Darwin.Application.Features.Auth.Login;

public static class Login
{
    public record Command(LoginRequest Model) : ICommand<DarwinResponse<TokenResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly IAuthService _authService;

        public CommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var token= await _authService.LoginAsync(request.Model);

            return DarwinResponse<TokenResponse>.Success(token, 200);
        }
    }
}

