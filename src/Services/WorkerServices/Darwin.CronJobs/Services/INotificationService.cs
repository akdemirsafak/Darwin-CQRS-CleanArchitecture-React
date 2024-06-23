
namespace Darwin.CronJobs.Services;

public interface INotificationService
{
    Task SendNew5ContentsAsync(List<string> to, string title, string body);
}
