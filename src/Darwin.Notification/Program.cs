using Darwin.Notification.Consumers;
using Darwin.Notification.Services;
using Darwin.Notification.Settings;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Buraya jwt


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserCreatedEventConsumer>();
    x.AddConsumer<ConfirmEmailEventConsumer>();
    x.AddConsumer<ResetPasswordEventConsumer>();
    x.AddConsumer<SendNewContentsEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:ConnectionString"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        cfg.ReceiveEndpoint("user-created-event-queue", e =>
        {
            e.ConfigureConsumer<UserCreatedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("confirm-email-event-queue", e =>
        {
            e.ConfigureConsumer<ConfirmEmailEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("reset-password-event-queue", e =>
        {
            e.ConfigureConsumer<ResetPasswordEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("send-newcontents-queue", e =>
        {
            e.ConfigureConsumer<SendNewContentsEventConsumer>(context);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
