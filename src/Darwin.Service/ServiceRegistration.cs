using Darwin.Core.Entities;
using Darwin.Infrastructure.DbContexts;
using Darwin.Service.Features.Moods.Commands;
using Darwin.Service.Localizations;
using Darwin.Service.TokenOperations;
using Darwin.Service.UserHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Darwin.Service;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateMood.Command)));
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

        serviceCollection.Configure<AppTokenOptions>(configuration.GetSection("AppTokenOptions"));
    }
}
