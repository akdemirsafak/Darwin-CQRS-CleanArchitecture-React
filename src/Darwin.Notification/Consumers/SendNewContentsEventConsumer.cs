using Darwin.Notification.Services;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Notification.Consumers;

public class SendNewContentsEventConsumer : IConsumer<SendNewContentsEvent>
{
    private readonly IEmailService _emailService;

    public SendNewContentsEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<SendNewContentsEvent> context)
    {
        await _emailService.SendNewContentsAsync(context.Message);
    }
}
