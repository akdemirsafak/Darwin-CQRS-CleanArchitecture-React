using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Service.Behaviors;
using Darwin.Contents.Service.Features.Categories.Commands;
using Darwin.Contents.Service.Mappers;
using Darwin.Contents.Service.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Darwin.Contents.Service;

public static class DIManager
{
    public static void RegisterServices(this IServiceCollection services)
    {

        services.AddScoped<IContentService, ContentService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IMoodService, MoodService>();

        services.AddAutoMapper(typeof(ContentMapper));


        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateCategory.CommandHandler).Assembly);
        });


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    }
}
