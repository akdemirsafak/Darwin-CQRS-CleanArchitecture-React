using Darwin.Presentation;
using Darwin.WebApi.Configurations;
using FluentValidation;
using Hangfire;
using MassTransit;
using Microsoft.Extensions.DependencyModel;
using Scrutor;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddHttpClientServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration);


var assemblies = DependencyContext.Default.RuntimeLibraries
    .Where(library => library.Name.StartsWith("Darwin"))
    .Select(library => Assembly.Load(new AssemblyName(library.Name)))
    .ToList();

builder.Services.Scan(scan => scan.FromAssemblies(assemblies)
        .AddClasses()
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .WithScopedLifetime());


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:ConnectionString"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });
    });
});




builder.Host.UseSerilog();


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
