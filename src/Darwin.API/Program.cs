using Darwin.API.Middlewares;
using Darwin.Infrastructure;
using Darwin.Model;
using Darwin.Service;
using Microsoft.AspNetCore.RateLimiting;
using Sentry;
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
builder.Services.AddSwaggerGen();


builder.Services.AddService(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
     options.AddDefaultPolicy(builder =>
     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));



var app = builder.Build();

app.UseRateLimiter();

app.UseGlobalExceptionMiddleware();

app.UseSentryTracing();


app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
