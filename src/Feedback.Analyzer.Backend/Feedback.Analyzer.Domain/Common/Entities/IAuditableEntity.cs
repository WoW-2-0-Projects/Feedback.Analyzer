namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Interface that marks an entity as being auditable for creation, modification, and deletion tracking.
/// Inherits from the `ISoftDeletedEntity` interface, which likely provides additional properties for soft deletion tracking.
/// </summary>
public interface IAuditableEntity : ISoftDeletedEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// Can be null if the entity has never been modified.
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }
}
