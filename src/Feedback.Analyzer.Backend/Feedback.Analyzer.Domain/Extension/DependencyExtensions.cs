namespace Feedback.Analyzer.Domain.Extension;

/// <summary>
/// Contains dependency extensions
/// </summary>
public static class DependencyExtensions
{
    public static object CreateInstance(this Type type, IServiceProvider serviceProvider)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        // Get public constructor
        var constructor = type.GetConstructors().FirstOrDefault(constructor => constructor.IsPublic);
        if (constructor is null)
            throw new InvalidOperationException();
        
        // Get constructor parameters
        var parameters = constructor.GetParameters();
        var dependencies = parameters.Select(parameter => serviceProvider.GetService(parameter.ParameterType)).ToArray();
        
        // Create instance
        return Activator.CreateInstance(type, dependencies)!;
    }
}