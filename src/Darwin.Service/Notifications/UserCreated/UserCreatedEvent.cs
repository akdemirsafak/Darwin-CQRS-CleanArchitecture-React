using Darwin.Core.Entities;
using Darwin.Service.EmailServices;
using MediatR;

namespace Darwin.Service.Notifications.UserCreated;

public record UserCreatedEvent(UserCreatedMailModel userCreatedMailModel) : INotification;

internal sealed class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IEmailService _emailService;

    public UserCreatedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _emailService.SendWellcomeWithConfirmationAsync(notification.userCreatedMailModel);
    }
}
