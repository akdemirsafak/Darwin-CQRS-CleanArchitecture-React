﻿using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Infrastructure.DbContexts;
using Darwin.Infrastructure.Repository;
using Darwin.Service.Localizations;
using Darwin.Service.Musics.Commands.Create;
using Darwin.Service.TokenOperations;
using Darwin.Service.Uof;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sentry;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//SENTRY
builder.WebHost.UseSentry(options =>
    options.ConfigureScope(scope =>
    {
        scope.Level = SentryLevel.Debug;
    }));

//SentrySdk.CaptureMessage("Hello Sentry");

//SENTRY END

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

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DarwinDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"),
    option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(DarwinDbContext))!.GetName().Name); });
});

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<DarwinDbContext>()
.AddErrorDescriber<LocalizationsIdentityErrorDescriber>()
.AddDefaultTokenProviders();

var tokenOptions = builder.Configuration.GetSection("AppTokenOptions").Get<AppTokenOptions>();

builder.Services.AddAuthentication(options =>
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

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.Configure<AppTokenOptions>(builder.Configuration.GetSection("AppTokenOptions"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateMusicCommand)));

builder.Services.AddCors(options =>
     options.AddDefaultPolicy(builder =>
     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddAuthentication();


var app = builder.Build();

app.UseRateLimiter();

//SENTRY Middleware

app.UseSentryTracing();

/// SENTRY Middleware End

app.UseCors();

// Configure the HTTP request pipeline.
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
