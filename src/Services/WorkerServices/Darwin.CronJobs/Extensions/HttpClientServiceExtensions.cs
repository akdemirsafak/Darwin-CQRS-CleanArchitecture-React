using Darwin.CronJobs.Services;
using Darwin.Shared.Utils;

namespace Darwin.CronJobs.Extensions;

public static class HttpClientServiceExtensions
{
    public static void AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<IUserService, UserService>(client =>
        {
            client.BaseAddress = new Uri(ServicePaths.UserService);
        });

        services.AddHttpClient<IContentService, ContentService>(client =>
        {
            client.BaseAddress = new Uri(ServicePaths.ContentService);
        });

        services.AddScoped<INotificationService, NotificationService>();
    }
}
