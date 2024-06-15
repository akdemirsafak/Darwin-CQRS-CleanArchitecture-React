using Darwin.Contents.API.Extensions;
using Darwin.Contents.Core.AbstractRepositories;
using Darwin.Contents.Repository.DbContexts;
using Darwin.Contents.Repository.Interceptors;
using Darwin.Contents.Repository.Repositories;
using Darwin.Contents.Service;
using Darwin.Shared.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.RegisterServices();

builder.Services.AddDbContext<AppDbContext>((sp, opt) =>
{
    var interceptor=sp.GetService<UpdateAuditableEntitiesInterceptor>()!;
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name); });
    opt.AddInterceptors(interceptor);
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Auth:Issuer"],
            ValidAudience = builder.Configuration["Auth:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Auth:SymmetricKey"]))
        };
    });

builder.Services.AddScoped<ICurrentUser, CurrentUser>();


builder.Services.AddHttpClientServices();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

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
