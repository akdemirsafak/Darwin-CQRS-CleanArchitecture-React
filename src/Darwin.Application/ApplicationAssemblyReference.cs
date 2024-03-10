using System.Reflection;

namespace Darwin.Application;

public static class ApplicationAssemblyReference
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
