using Darwin.Application.Common;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Application.Features.Users.Commands.ResetPassword;

public static class ResetPasswordEmail
{
    public record Command(string Email) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHanler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IUserService _userService;
        private readonly ILinkCreator _linkCreator;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CommandHanler(IUserService userService,
            ILinkCreator linkCreator,
            ISendEndpointProvider sendEndpointProvider
            )
        {
            _userService = userService;
            _linkCreator = linkCreator;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByEmailAsync(request.Email);
            var resetPasswordToken = await _userService.GeneratePasswordResetTokenAsync(user);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ResetPasswordVerify", "Auth", user.Id, resetPasswordToken);


            var resetPasswordEvent = new ResetPasswordEvent
            {
                To = user.Email,
                Url = confirmationUrl
            };

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:reset-password-event-queue"));
            await sendEndpoint.Send(resetPasswordEvent, cancellationToken);

            return DarwinResponse<NoContent>.Success();
        }
    }
}
