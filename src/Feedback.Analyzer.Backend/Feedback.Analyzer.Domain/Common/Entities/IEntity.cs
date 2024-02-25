namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents a base entity within the system, characterized by a unique identifier.
/// Concrete implementations of this interface should define additional properties and behaviors specific to their entity type.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity. This identifier should be globally unique within the system.
    /// </summary>
    /// <value>
    /// The unique identifier of the entity.
    /// </value>
    public Guid Id { get; set; }
}
