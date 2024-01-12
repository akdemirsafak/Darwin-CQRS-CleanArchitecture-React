using Darwin.Infrastructure;
using Darwin.Service;
using Hangfire;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using Sentry;
using Serilog;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSentry(options =>
    options.ConfigureScope(scope =>
    {
        scope.Level = SentryLevel.Debug;
    }));

builder.Services.AddControllers();
builder.Services.AddRateLimiter(options =>
{
    options.AddTokenBucketLimiter("TokenBucket", _options =>
    {
        _options.TokenLimit = 10;
        _options.TokensPerPeriod = 3;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        _options.QueueLimit = 5;
        _options.ReplenishmentPeriod = TimeSpan.FromSeconds(30);
    });
    options.RejectionStatusCode = 429;
});

builder.Services.AddEndpointsApiExplorer();
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


builder.Services.AddService(builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
     options.AddDefaultPolicy(builder =>
     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


//builder.Services.AddSwaggerGen(); //

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseRateLimiter();

//app.UseGlobalExceptionMiddleware();

app.UseSentryTracing();

app.UseStaticFiles();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
