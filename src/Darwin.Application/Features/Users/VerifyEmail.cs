using Darwin.Application.Common;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Application.Features.Users;

public static class VerifyEmail
{
    public record Command() : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;
        private readonly ILinkCreator _linkCreator;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CommandHandler(IUserService userService,
            ICurrentUser currentUser, 
            ILinkCreator linkCreator,
            ISendEndpointProvider sendEndpointProvider)
        {
            _userService = userService;
            _currentUser = currentUser;
            _linkCreator = linkCreator;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user= await _userService.FindByIdAsync(_currentUser.GetUserId);
            var confirmationToken=await _userService.GenerateEmailConfirmationTokenAsyncByUserIdAsync(_currentUser.GetUserId);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "Auth", _currentUser.GetUserId, confirmationToken);

            
            var verifyEmailMessage=new VerifyEmailEvent
            {
                FullName = $"{user.Name} {user.LastName}",
                UserName = user.UserName,
                To = user.Email!,
                ConfirmationUrl = confirmationUrl
            };

            var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:verify-email-event-queue"));
            await sendEndpoint.Send<VerifyEmailEvent>(verifyEmailMessage, cancellationToken);

            return DarwinResponse<NoContent>.Success();
        }
    }
}
