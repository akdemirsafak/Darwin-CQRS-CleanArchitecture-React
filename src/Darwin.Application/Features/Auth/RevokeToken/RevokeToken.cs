using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Share.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Auth.RevokeToken;

public static class RevokeToken
{
    public record Command(string Email) : ICommand<DarwinResponse<NoContentDto>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly IAuthService _authService;

        public CommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _authService.RevokeTokenByEmailAsync(request.Email);
            return DarwinResponse<NoContentDto>.Success();
        }
    }
    public class RevokeTokenCommandValidator : AbstractValidator<Command>
    {
        public RevokeTokenCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}