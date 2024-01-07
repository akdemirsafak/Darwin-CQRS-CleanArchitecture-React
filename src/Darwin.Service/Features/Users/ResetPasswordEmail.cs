using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Service.Common;
using Darwin.Service.EmailServices;
using Darwin.Service.Helper;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users;

public static class ResetPasswordEmail
{
    public record Command(string Email):ICommand<DarwinResponse<NoContent>>;

    public class CommandHanler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILinkCreator _linkCreator;
        private readonly IEmailService _emailService;
        public CommandHanler(UserManager<AppUser> userManager, 
            ILinkCreator linkCreator, 
            IEmailService emailService)
        {
            _userManager = userManager;
            _linkCreator = linkCreator;
            _emailService = emailService;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            AppUser? user= await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return DarwinResponse<NoContent>.Fail("User not found.");
            }
            var resetPasswordToken=await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ResetPasswordVerify", "Auth", user.Id, resetPasswordToken);

            await _emailService.SendResetPasswordMailAsync(user.Email!, confirmationUrl);
            return DarwinResponse<NoContent>.Success();
        }
    }
}
