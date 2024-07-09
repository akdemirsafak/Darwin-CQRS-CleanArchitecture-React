using Darwin.Notification.Services;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Notification.Consumers;

public class UserCreatedEventConsumer : IConsumer<UserCreatedSendNotificationEvent>
{

    private readonly IEmailService _emailService;

    public UserCreatedEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<UserCreatedSendNotificationEvent> context)
    {
        await _emailService.SendWellcomeEmailAsync(context.Message);

    }
}
