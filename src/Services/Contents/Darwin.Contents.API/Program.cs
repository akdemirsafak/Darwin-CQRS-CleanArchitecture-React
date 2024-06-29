using Darwin.Contents.API.Extensions;
using Darwin.Contents.API.Middlewares;
using Darwin.Contents.Core.AbstractRepositories;
using Darwin.Contents.Repository.DbContexts;
using Darwin.Contents.Repository.Interceptors;
using Darwin.Contents.Repository.Repositories;
using Darwin.Contents.Service;
using Darwin.Shared.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterServices();

builder.Services.AddScoped<UpdateAuditableEntitiesInterceptor>();
string connectionString=builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AppDbContext>((sp, opt) =>
{
    var interceptor=sp.GetService<UpdateAuditableEntitiesInterceptor>()!;
    opt.UseSqlServer(connectionString,
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


builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Darwin Content Api"
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
