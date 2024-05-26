using Darwin.Shared.Events;

namespace Darwin.Notification.Services;

public interface IEmailService
{
    Task SendVerifyEmailAsync(VerifyEmailEvent verifyEmailMessage);
    Task SendWellcomeEmailAsync(UserCreatedEvent mailDetails);
    Task SendNewContentsAsync(SendNewContentsEvent sendNewContentsEvent);
    Task SendResetPasswordMailAsync(string To, string resetPasswordTokenAddress);
}
