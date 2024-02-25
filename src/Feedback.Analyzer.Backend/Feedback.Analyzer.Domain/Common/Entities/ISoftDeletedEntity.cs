namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents an entity that supports soft deletion, inheriting properties from IEntity.
/// </summary>
/// <summary>
/// Represents an entity that supports soft deletion. Entities implementing this interface can be marked as deleted
/// without physically removing them from the database, allowing for potential recovery or historical tracking.
/// </summary>
public interface ISoftDeletedEntity : IEntity
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
