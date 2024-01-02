using Darwin.Service.EmailServices;
using MediatR;
using Polly;
using Polly.Retry;

namespace Darwin.Service.Events.UserCreated;

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
        AsyncRetryPolicy policy=Policy.Handle<Exception>()
            .WaitAndRetryAsync(3, attempt=> TimeSpan.FromSeconds(10));

        await policy.ExecuteAndCaptureAsync(() =>
        _emailService.SendWellcomeWithConfirmationAsync(notification.userCreatedMailModel)
        );
        PolicyResult result= await policy.ExecuteAndCaptureAsync(() =>
        _emailService.SendWellcomeWithConfirmationAsync(notification.userCreatedMailModel)
        );


        //await _emailService.SendWellcomeWithConfirmationAsync(notification.userCreatedMailModel);
    }
}