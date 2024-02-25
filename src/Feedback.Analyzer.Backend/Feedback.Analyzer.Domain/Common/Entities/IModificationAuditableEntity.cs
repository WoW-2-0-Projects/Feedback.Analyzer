namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents an entity that tracks its modification history by storing the identifier of the user who last modified it.
/// Entities implementing this interface enable auditing and traceability of changes within the system.
/// </summary>
public interface IModificationAuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the user who last modified the entity.
    /// This value is typically set automatically by the system when the entity is updated.
    /// </summary>
    /// <value>
    /// The identifier of the user who last modified the entity.
    /// </value>
    public Guid ModifiedBUserId { get; set; }
}
