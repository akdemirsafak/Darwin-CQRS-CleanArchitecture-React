using Darwin.Application.Services;
using Darwin.Infrastructure.Services;
using Darwin.Persistance.Services;
using Darwin.Shared.Utils;

namespace Darwin.WebApi.Configurations;

public static class ServiceDIExtensions
{
    public static void AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAzureBlobStorageService, AzureBlobStorageService>(opt =>
        {
            opt.BaseAddress = new Uri(ServicePaths.FileService);
        });
        services.AddHttpClient<IFileService, FileService>(opt =>
        {
            opt.BaseAddress = new Uri(ServicePaths.FileService);
        });
    }
}
