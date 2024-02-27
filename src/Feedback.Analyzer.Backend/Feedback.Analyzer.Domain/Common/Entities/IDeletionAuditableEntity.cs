namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Interface that marks an entity as being auditable for deletion tracking.
/// </summary>
public interface IDeletionAuditableEntity
{
    /// <summary>
    /// Gets or sets the ID of the user who deleted the entity.
    /// </summary>
    Guid? DeletedByUserId { get; set; }
}
