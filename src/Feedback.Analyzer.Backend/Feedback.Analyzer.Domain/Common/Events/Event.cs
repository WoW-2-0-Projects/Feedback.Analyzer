namespace Feedback.Analyzer.Domain.Common.Events;

/// <summary>
/// Represents an implementation of the IEvent interface, defining the properties for an event.
/// </summary>
public record Event: IEvent
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Gets or sets the timestamp when the event was created in Coordinated Universal Time (UTC).
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    
    /// <summary>
    /// Gets or sets a value indicating whether the event has been redelivered.
    /// </summary>
    public bool Redelivered { get; set; }
}