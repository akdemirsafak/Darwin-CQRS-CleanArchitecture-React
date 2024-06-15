using Darwin.AuthServer.DbContexts;
using Darwin.AuthServer.Entities;
using Darwin.AuthServer.Helper;
using Darwin.AuthServer.Interceptors;
using Darwin.AuthServer.Services;
using Darwin.Shared.Options;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServer:Authority"];
    options.Audience = builder.Configuration["IdentityServer:Audiences"];
    options.RequireHttpsMetadata = false;
});


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILinkCreator, LinkCreator>();


builder.Services.Configure<AppTokenOptions>(builder.Configuration.GetSection("TokenOptions"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<AuditInterceptor>();

IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
builder.Services.AddDbContext<AuthServerDbContext>((sp, options) =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    var interceptor=sp.GetService<AuditInterceptor>()!;
    options.AddInterceptors(interceptor);
});
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AuthServerDbContext>()
    .AddDefaultTokenProviders();


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
