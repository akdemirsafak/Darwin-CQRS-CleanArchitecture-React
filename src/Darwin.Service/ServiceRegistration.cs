using Azure.Storage.Blobs;
using Darwin.Core.Entities;
using Darwin.Core.ServiceCore;
using Darwin.Infrastructure.DbContexts;
using Darwin.Service.Behaviors;
using Darwin.Service.Configures;
using Darwin.Service.EmailServices;
using Darwin.Service.Features.Moods.Commands;
using Darwin.Service.Helper;
using Darwin.Service.Jobs;
using Darwin.Service.Localizations;
using Darwin.Service.Services;
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

        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(CreateMood.Command));
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        var tokenOptions = configuration.GetSection("AppTokenOptions").Get<AppTokenOptions>();


        //serviceCollection.AddIndentityCore<AppUser,AppRole>().AddRoles<AppRole>(); 
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
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions!.Issuer,
                ValidAudiences = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            };
        });

        serviceCollection.AddScoped<ICurrentUser, CurrentUser>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<ILinkCreator, LinkCreator>();


        serviceCollection.AddSingleton<IAzureBlobStorageService, AzureBlobStorageService>();

        serviceCollection.Configure<AppTokenOptions>(configuration.GetSection("AppTokenOptions"));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341/")
            .WriteTo.File("logs/myBeatifulLog-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        serviceCollection.AddScoped<IEmailService, EmailService>();
        serviceCollection.AddScoped<IFileService, FileService>();


        serviceCollection.AddHangfire(x =>
        {
            x.UseSqlServerStorage(configuration.GetConnectionString("HangfireJobsConnection"));

            RecurringJob.AddOrUpdate<WeeklyContents>(j => j.SendNew5Contents(),
                Cron.Weekly(DayOfWeek.Friday, 12), TimeZoneInfo.Local);
        });
        serviceCollection.AddHangfireServer();

        serviceCollection.AddSingleton(new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorageConnectionString")));


        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["RedisCacheSettings:ConnectionString"];
            options.Configuration = configuration["RedisCacheSettings:InstanceName"];
        });
        serviceCollection.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBehavior<,>));
        serviceCollection.AddScoped<IRedisCacheService, RedisCacheService>();

    }
}
