using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Service.Services;
using Darwin.Shared.Utils;

namespace Darwin.Contents.API.Extensions;

public static class ServiceDIExtension
{
    public static void AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<IFileService, FileService>(opt =>
        {
            opt.BaseAddress = new Uri(ServicePaths.FileService);
        });
    }
}
