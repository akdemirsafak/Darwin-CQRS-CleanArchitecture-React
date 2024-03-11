using Microsoft.OpenApi.Models;
using Serilog;

namespace Darwin.WebApi.Configurations;

public static class PresentationServiceInstaller
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        string logdbConnectionString=configuration.GetConnectionString("LogDbConnection");
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .Enrich.FromLogContext()
            .WriteTo.Seq("http://localhost:5341/")
            .WriteTo.MSSqlServer(logdbConnectionString, tableName: "Logs", autoCreateSqlTable: true)
            .WriteTo.File("logs/myBeatifulLog-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Darwin Api"
            });
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Bearerdan sonra boşluk sonra token"

            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
        });



        services.AddCors(_ =>
        {
            _.AddDefaultPolicy(policy =>
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(policy => true)
                );
        });
    }
}
