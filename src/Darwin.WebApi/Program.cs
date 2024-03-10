using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Infrastructure.EmailServices;
using Darwin.Persistance.Helper;
using Darwin.Presentation;
using Darwin.WebApi.Configurations;
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Microsoft.OpenApi.Models;
using Scrutor;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();




builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);




builder.Services.AddScoped<ILinkCreator, LinkCreator>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();




//var emailConfig = builder.Configuration
//        .GetSection("EmailSettings")
//        .Get<EmailSettings>();
//builder.Services.AddSingleton(emailConfig);

string logdbConnectionString=builder.Configuration.GetConnectionString("LogDbConnection");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .Enrich.FromLogContext()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.MSSqlServer(logdbConnectionString, tableName: "Logs", autoCreateSqlTable: true)
    .WriteTo.File("logs/myBeatifulLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSwaggerGen(x =>
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





var assemblies = DependencyContext.Default.RuntimeLibraries
    .Where(library => library.Name.StartsWith("Darwin"))
    .Select(library => Assembly.Load(new AssemblyName(library.Name)))
    .ToList();

builder.Services.Scan(scan => scan.FromAssemblies(assemblies)
        .AddClasses()
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .WithScopedLifetime());




builder.Services.AddCors(_ =>
{
    _.AddDefaultPolicy(policy =>
            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(policy => true)
        );
});
//builder.Services.AddCors(options =>
//     options.AddDefaultPolicy(builder =>
//     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
            .AddApplicationPart(typeof(PresentationAssemblyReference).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();


app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.UseHangfireServer();

app.Run();
