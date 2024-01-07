using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Service.Common;
using Darwin.Service.EmailServices;
using Darwin.Service.Helper;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users;

public static class VerifyEmail
{
    public record Command():ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrentUser _currentUser;
        private readonly IEmailService _emailService;
        private readonly ILinkCreator _linkCreator;

        public CommandHandler(UserManager<AppUser> userManager, 
            ICurrentUser currentUser, 
            IEmailService emailService, 
            ILinkCreator linkCreator)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _emailService = emailService;
            _linkCreator = linkCreator;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            AppUser? user= await _userManager.FindByIdAsync(_currentUser.GetUserId);
            if (user == null)
            {
                return DarwinResponse<NoContent>.Fail("User not found.");
            }
            var confirmationToken=await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "Auth", user.Id, confirmationToken);

            await _emailService.SendConfirmMailAsync(user.Email!, confirmationUrl);

            return DarwinResponse<NoContent>.Success();
        }
    }
}
