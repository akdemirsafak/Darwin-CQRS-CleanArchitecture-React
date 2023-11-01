using Darwin.Service.Configures;

namespace Darwin.Service.EmailServices;

public interface IEmailService
{
    Task SendWeeklySuggestionsAsync(SendEmailModel model);
}
