namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Interface that marks an entity as being auditable for creation tracking.
/// </summary>
public interface ICreationAuditableEntity
{
    /// <summary>
    /// Gets or sets the ID of the user who created the entity.
    /// </summary>
    Guid CreatedByUserId { get; set; }
}
