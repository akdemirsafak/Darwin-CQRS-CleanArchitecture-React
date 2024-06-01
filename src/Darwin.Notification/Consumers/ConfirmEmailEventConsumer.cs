using Darwin.Notification.Services;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Notification.Consumers;

public class ConfirmEmailEventConsumer : IConsumer<ConfirmEmailEvent>
{
    private readonly IEmailService _emailService;

    public ConfirmEmailEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<ConfirmEmailEvent> context)
    {
        await _emailService.SendConfirmEmailAsync(context.Message);
    }
}
