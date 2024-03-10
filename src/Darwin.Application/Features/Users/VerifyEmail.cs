using Darwin.Application.Common;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;

namespace Darwin.Application.Features.Users;

public static class VerifyEmail
{
    public record Command() : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;
        private readonly IEmailService _emailService;
        private readonly ILinkCreator _linkCreator;

        public CommandHandler(IUserService userService, ICurrentUser currentUser, IEmailService emailService, ILinkCreator linkCreator)
        {
            _userService = userService;
            _currentUser = currentUser;
            _emailService = emailService;
            _linkCreator = linkCreator;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user= await _userService.FindByIdAsync(_currentUser.GetUserId);
            var confirmationToken=await _userService.GenerateEmailConfirmationTokenAsyncByUserIdAsync(_currentUser.GetUserId);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "Auth", _currentUser.GetUserId, confirmationToken);

            await _emailService.SendConfirmMailAsync(user.Email!, confirmationUrl);

            return DarwinResponse<NoContent>.Success();
        }
    }
}
