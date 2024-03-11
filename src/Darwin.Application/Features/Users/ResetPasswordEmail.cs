using Darwin.Application.Common;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;

namespace Darwin.Application.Features.Users;

public static class ResetPasswordEmail
{
    public record Command(string Email) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHanler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IUserService _userService;
        private readonly ILinkCreator _linkCreator;
        private readonly IEmailService _emailService;

        public CommandHanler(IUserService userService,
            ILinkCreator linkCreator,
            IEmailService emailService)
        {
            _userService = userService;
            _linkCreator = linkCreator;
            _emailService = emailService;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user=await _userService.FindByEmailAsync(request.Email);
            var resetPasswordToken=await _userService.GeneratePasswordResetTokenAsync(user);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ResetPasswordVerify", "Auth", user.Id, resetPasswordToken);
            await _emailService.SendResetPasswordMailAsync(user.Email!, confirmationUrl);
            return DarwinResponse<NoContent>.Success();
        }
    }
}
