using Darwin.Application;
using Darwin.Application.Behaviors;
using Darwin.Application.CronJobs;
using Darwin.Persistance.Options;
using FluentValidation;
using Hangfire;
using MediatR;

namespace Darwin.WebApi.Configurations;

public static class ApplicationServiceInstaller
{
    public static void AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {

            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });


        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["RedisCacheSettings:ConnectionString"];
            options.Configuration = configuration["RedisCacheSettings:InstanceName"];
        });
        services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));



        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBehavior<,>));


        services.AddHangfire(x =>
        {
            x.UseSqlServerStorage(configuration.GetConnectionString("HangfireJobsConnection"));

            RecurringJob.AddOrUpdate<WeeklyContents>(j => j.SendNew5Contents(),
                Cron.Weekly(DayOfWeek.Friday, 12), TimeZoneInfo.Local);
        });
        services.AddHangfireServer();



    }
}
