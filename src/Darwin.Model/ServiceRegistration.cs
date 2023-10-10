using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Darwin.Model;

public static class ServiceRegistration
{
    public static void AddModels(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
