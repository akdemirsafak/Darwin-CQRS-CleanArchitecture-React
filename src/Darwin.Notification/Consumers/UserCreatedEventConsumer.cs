using Darwin.Notification.Services;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Notification.Consumers;

public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{

    private readonly IEmailService _emailService;

    public UserCreatedEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        await _emailService.SendWellcomeEmailAsync(context.Message);

    }
}
