namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents an entity that combines soft deletion with auditing capabilities. This class inherits from both
/// `SoftDeletedEntity` and `IModificationAuditableEntity`, providing properties for tracking creation, modification,
/// and soft deletion timestamps, as well as the identifier of the user who last modified the entity.
/// </summary>
public class AuditableEntity : SoftDeletedEntity, IAuditableEntity
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
