using Darwin.Core.Entities;
using Darwin.Infrastructure.DbContexts;
using Darwin.Service.Behaviors;
using Darwin.Service.Configures;
using Darwin.Service.EmailServices;
using Darwin.Service.Features.Moods.Commands;
using Darwin.Service.Helper;
using Darwin.Service.Jobs;
using Darwin.Service.Localizations;
using Darwin.Service.TokenOperations;
using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;

namespace Darwin.Service;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateMood.Command)));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        var tokenOptions = configuration.GetSection("AppTokenOptions").Get<AppTokenOptions>();

        serviceCollection.AddIdentity<AppUser, AppRole>(x =>
        {
            x.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<DarwinDbContext>()
        .AddErrorDescriber<LocalizationsIdentityErrorDescriber>()
        .AddDefaultTokenProviders();

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions!.Issuer,
                ValidAudience = tokenOptions.Audience[0],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            };
        });

        serviceCollection.AddScoped<ICurrentUser, CurrentUser>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<ILinkCreator, LinkCreator>();

        serviceCollection.Configure<AppTokenOptions>(configuration.GetSection("AppTokenOptions"));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341/")
            .WriteTo.File("logs/myBeatifulLog-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        serviceCollection.AddScoped<IEmailService, EmailService>();


        serviceCollection.AddHangfire(x =>
        {
            x.UseSqlServerStorage(configuration.GetConnectionString("HangfireJobsConnection"));

            RecurringJob.AddOrUpdate<WeeklyContents>(j => j.SendNew5Contents(),
                Cron.Weekly(DayOfWeek.Friday, 12), TimeZoneInfo.Local);
        });
        serviceCollection.AddHangfireServer();
    }
}
