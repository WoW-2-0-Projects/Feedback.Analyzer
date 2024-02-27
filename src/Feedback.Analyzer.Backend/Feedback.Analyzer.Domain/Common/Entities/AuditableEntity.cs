namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents an abstract class providing audit functionality for entities, inheriting properties from SoftDeletedEntity and implementing IAuditableEntity.
/// </summary>
public abstract class AuditableEntity : SoftDeletedEntity, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created. This value is typically set automatically by the
    /// system when the entity is first persisted.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated. This value is typically updated automatically
    /// by the system whenever changes are made to the entity's properties.
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }
}
