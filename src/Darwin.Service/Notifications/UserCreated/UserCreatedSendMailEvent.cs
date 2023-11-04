using Darwin.Service.EmailServices;
using MediatR;

namespace Darwin.Service.Notifications.UserCreated;

public record UserCreatedSendMailEvent(UserCreatedMailModel userCreatedMailModel) : INotification;

internal sealed class UserCreatedEventHandler : INotificationHandler<UserCreatedSendMailEvent>
{
    private readonly IEmailService _emailService;

    public UserCreatedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UserCreatedSendMailEvent notification, CancellationToken cancellationToken)
    {
        await _emailService.SendWellcomeWithConfirmationAsync(notification.userCreatedMailModel);
    }
}
