using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Darwin.Model;

public static class ServiceRegistration
{
    public static void AddModels(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
