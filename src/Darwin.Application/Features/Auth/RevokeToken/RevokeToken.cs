using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using FluentValidation;

namespace Darwin.Application.Features.Auth.RevokeToken;

public static class RevokeToken
{
    public record Command(string Email) : ICommand<DarwinResponse<NoContent>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IAuthService _authService;

        public CommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _authService.RevokeTokenByEmailAsync(request.Email);
            return DarwinResponse<NoContent>.Success();
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