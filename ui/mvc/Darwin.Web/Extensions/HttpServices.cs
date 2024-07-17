using Darwin.Web.Services;
using Darwin.Web.Settings;

namespace Darwin.Web.Extensions;

public static class HttpServices
{
    public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));
        var serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

        // Gateway'e istek yapar hale gelelim.
        services.AddHttpClient<IContentService, ContentService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUrl}/{serviceApiSettings.Content.Path}");
        });

        services.AddHttpClient<IPlanService, PlanService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUrl}/{serviceApiSettings.Plan.Path}");
        });

        services.AddHttpClient<IMoodService, MoodService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUrl}/{serviceApiSettings.Mood.Path}");
        });
        services.AddHttpClient<ICategoryService, CategoryService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUrl}/{serviceApiSettings.Category.Path}");
        });
        services.AddHttpClient<IAuthService, AuthService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUrl}/{serviceApiSettings.Auth.Path}");
        });
        services.AddHttpClient<IUserService, UserService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.BaseUrl}/{serviceApiSettings.User.Path}");
        });

    }
}


