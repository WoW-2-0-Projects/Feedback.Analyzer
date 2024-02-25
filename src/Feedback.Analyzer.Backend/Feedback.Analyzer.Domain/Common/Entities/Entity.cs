namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents a basic entity in the system with a unique identifier.
/// This class inherits from the IEntity interface and implements its Id property.
/// Use this class as a base for more specific entity types that require additional properties and behaviors.
/// </summary>
public class Entity : IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity. This identifier should be globally unique within the system.
    /// </summary>
    /// <value>
    /// The unique identifier of the entity.
    /// </value>
    public Guid Id { get; set; }
}
