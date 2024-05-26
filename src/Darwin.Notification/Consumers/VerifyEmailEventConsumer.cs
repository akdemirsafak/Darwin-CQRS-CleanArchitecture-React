using Darwin.Notification.Services;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Notification.Consumers;

public class VerifyEmailEventConsumer : IConsumer<VerifyEmailEvent>
{
    private readonly IEmailService _emailService;

    public VerifyEmailEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<VerifyEmailEvent> context)
    {
        await _emailService.SendVerifyEmailAsync(context.Message);
    }
}
