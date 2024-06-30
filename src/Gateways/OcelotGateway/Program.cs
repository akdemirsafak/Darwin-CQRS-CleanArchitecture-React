using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("GatewayAuthenticationScheme", opt => //Bu scheme name config dosyasında hangi root'a eklersek o token ile korunacak.
    {
        opt.Authority = builder.Configuration.GetConnectionString("AuthServer");
        opt.Audience = "https://localhost:7000";
        opt.RequireHttpsMetadata = true;
    });


builder.Services.AddHttpContextAccessor();

builder.Services.AddOcelot();

builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();



var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
await app.UseOcelot();
app.Run();
