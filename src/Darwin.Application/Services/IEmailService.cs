using Darwin.Application.Events.UserCreated;
using Darwin.Domain.Dtos;

namespace Darwin.Application.Services;

public interface IEmailService
{
    Task SendNew5ContentsAsync(SendEmailModel model);
    Task SendWellcomeWithConfirmationAsync(UserCreatedMailModel model);
    Task SendConfirmMailAsync(string To, string confirmationTokenAddress);
    Task SendResetPasswordMailAsync(string To, string resetPasswordTokenAddress);
}
