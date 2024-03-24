using System.Reflection;

namespace Darwin.Presentation;

public static class PresentationAssemblyReference
{
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}
