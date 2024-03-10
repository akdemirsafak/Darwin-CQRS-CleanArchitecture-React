using System.Reflection;

namespace Darwin.Persistance;

public static class PersistanceAssemblyReference
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
