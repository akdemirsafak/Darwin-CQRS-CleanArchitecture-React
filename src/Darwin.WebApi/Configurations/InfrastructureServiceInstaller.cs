using Azure.Storage.Blobs;
using Darwin.Infrastructure.Options;
using Darwin.Service.TokenOperations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Darwin.WebApi.Configurations;

public static class InfrastructureServiceInstaller
{
    public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        var tokenOptions = configuration.GetSection("AppTokenOptions").Get<AppTokenOptions>();


        services.AddAuthentication(options =>
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
                ValidAudiences = tokenOptions.Audiences,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            };
        });
        services.Configure<AppTokenOptions>(configuration.GetSection("AppTokenOptions"));

        services.AddSingleton(new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorageConnectionString")));

    }
}
