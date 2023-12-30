using Darwin.Service.Configures;
using Darwin.Service.Events.UserCreated;

namespace Darwin.Service.EmailServices;

public interface IEmailService
{
    Task SendNew5ContentsAsync(SendEmailModel model);
    Task SendWellcomeWithConfirmationAsync(UserCreatedMailModel model);
}
