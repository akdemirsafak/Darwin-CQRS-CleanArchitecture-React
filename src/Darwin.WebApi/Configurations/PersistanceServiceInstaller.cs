using Darwin.Application.Helper;
using Darwin.Domain.Entities;
using Darwin.Domain.RepositoryCore;
using Darwin.Persistance.DbContexts;
using Darwin.Persistance.Helper;
using Darwin.Persistance.Interceptors;
using Darwin.Persistance.Repository;
using Darwin.Persistance.Repository.DapperRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Darwin.WebApi.Configurations;

public static class PersistanceServiceInstaller
{
    public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        string postgresqlConnectionString=configuration.GetConnectionString("PostgreConnection");
        services.AddDbContext<DarwinDbContext>((sp, opt) =>
        {
            var interceptor=sp.GetService<UpdateAuditableEntitiesInterceptor>()!;
            //var anotherinterceptor
            opt.UseNpgsql(postgresqlConnectionString,
            option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(DarwinDbContext))!.GetName().Name); }) //typeof(PersistanceAssemblyReference.Assembly).Assembly
            .AddInterceptors(interceptor);
            //If we have someinterceptors = AddInterceptors(interceptor1,interceptor2);
        });
        services.AddIdentity<AppUser, AppRole>(x =>
        {
            x.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<DarwinDbContext>()
        //.AddErrorDescriber<LocalizationsIdentityErrorDescriber>()
        .AddDefaultTokenProviders();


        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IMoodRepository, MoodRepository>();
        services.AddScoped<IPlayListRepository, PlayListRepository>();



        services.AddScoped<ILinkCreator, LinkCreator>();
        services.AddScoped<ICurrentUser, CurrentUser>();


    }
}
