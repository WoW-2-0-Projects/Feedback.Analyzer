using MediatR;

namespace Feedback.Analyzer.Domain.Common.Events;

/// <summary>
/// Represents a handler for a specific type of event.
/// </summary>
/// <typeparam name="TEvent">The type of event being handled.</typeparam>
public interface IEventHandler<in TEvent> : IEventHandler, INotificationHandler<TEvent> where TEvent : IEvent
{
    
}

/// <summary>
/// Marker interface for event handlers.
/// </summary>
public interface IEventHandler
{
    
}