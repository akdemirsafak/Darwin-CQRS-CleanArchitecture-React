using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Auth.RevokeToken;

public static class RevokeToken
{
    public record Command(string Email) : ICommand<DarwinResponse<NoContent>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;

        public CommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user= await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return DarwinResponse<NoContent>.Fail("User not found.");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

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