namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents a base class for entities that support soft deletion. This class inherits from both the `Entity` class
/// and the `ISoftDeletedEntity` interface, providing the basic implementation for soft deletion functionality.
/// Concrete implementations of this class should define specific entity properties and behaviors.
/// </summary>
public abstract class SoftDeletedEntity : Entity, ISoftDeletedEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity has been soft deleted.
    /// </summary>
    /// <value>
    /// <c>true</c> if the entity has been soft deleted; otherwise, <c>false</c>.
    /// </value>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was soft deleted. This value will be <c>null</c> if the entity
    /// has not been soft deleted.
    /// </summary>
    /// <value>
    /// The date and time when the entity was soft deleted, or <c>null</c> if not soft deleted.
    /// </value>
    public DateTimeOffset? DeletedTime { get; set; }
}
