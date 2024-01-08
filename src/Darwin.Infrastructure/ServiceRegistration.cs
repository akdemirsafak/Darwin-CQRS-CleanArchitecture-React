using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Infrastructure.DbContexts;
using Darwin.Infrastructure.Interceptors;
using Darwin.Infrastructure.Repository;
using Darwin.Infrastructure.Uof;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Darwin.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        serviceCollection.AddDbContext<DarwinDbContext>((sp, opt) =>
        {
            var interceptor=sp.GetService<UpdateAuditableEntitiesInterceptor>()!;
            //var anotherinterceptor
            opt.UseNpgsql(configuration.GetConnectionString("PostgreConnection"),
            option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(DarwinDbContext))!.GetName().Name); })
            .AddInterceptors(interceptor);
            //If we have someinterceptors = AddInterceptors(interceptor1,interceptor2);
        });

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped<IContentRepository, ContentRepository>();

    }
}
