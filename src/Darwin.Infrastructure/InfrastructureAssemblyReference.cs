using System.Reflection;

namespace Darwin.Presentation;

public static class InfrastructureAssemblyReference
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
