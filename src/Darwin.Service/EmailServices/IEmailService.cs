using Darwin.Service.Configures;

namespace Darwin.Service.EmailServices;

public interface IEmailService
{
    Task SendNew5ContentsAsync(SendEmailModel model);
}
