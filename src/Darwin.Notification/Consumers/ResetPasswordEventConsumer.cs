using Darwin.Notification.Services;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Notification.Consumers;

public class ResetPasswordEventConsumer : IConsumer<ResetPasswordEvent>
{
    private readonly IEmailService _emailService;

    public ResetPasswordEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<ResetPasswordEvent> context)
    {
        await _emailService.SendResetPasswordMailAsync(context.Message.To, context.Message.Url);
    }
}
