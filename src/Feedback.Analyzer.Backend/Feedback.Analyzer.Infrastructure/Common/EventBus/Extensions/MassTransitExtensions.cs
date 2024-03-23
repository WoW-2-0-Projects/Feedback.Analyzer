using System.Reflection;
using Feedback.Analyzer.Domain.Common.Events;
using MassTransit;

namespace Feedback.Analyzer.Infrastructure.Common.EventBus.Extensions;

/// <summary>
/// Provides extension methods for MassTransit.
/// </summary>
public static class MassTransitExtensions
{
    /// <summary>
    /// Registers all the consumers form the assembly
    /// </summary>
    /// <param name="busConfigurator">MassTransit bus configuration</param>
    /// <param name="assemblies">Collection of assemblies to get consumers from</param>
    public static void RegisterAllConsumers(this IBusRegistrationConfigurator busConfigurator, ICollection<Assembly> assemblies) 
    {
        // Get all implementations of consumer
        var consumers = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract 
                           && type.GetInterfaces().Any(implementedInterface => 
                               implementedInterface.IsGenericType 
                               && implementedInterface.GetGenericTypeDefinition() == typeof(IEventHandler<>)));
            
        foreach(var consumer in consumers)
            busConfigurator.AddConsumer(consumer);
    }
}