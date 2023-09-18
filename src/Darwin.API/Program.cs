using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Infrastructure;
using Darwin.Infrastructure.Repository;
using Darwin.Model.Mappers;
using Darwin.Service.Musics.Commands.Create;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sentry;
using System.Reflection;

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

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DarwinDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"),
    option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(DarwinDbContext))!.GetName().Name); });
});

builder.Services.AddIdentity<AppUser, AppRole>()
.AddEntityFrameworkStores<DarwinDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

builder.Services.AddMediatR(typeof(CreateMusicCommand).Assembly);

builder.Services.AddCors(options =>
     options.AddDefaultPolicy(builder =>
     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


builder.Services.AddAutoMapper(typeof(MusicMapper));
builder.Services.AddAuthentication();


var app = builder.Build();

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
