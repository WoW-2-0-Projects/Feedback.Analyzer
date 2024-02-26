using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents a product entity, inheriting auditable properties.
/// </summary>
public class Product : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the unique identifier of the organization associated with this product.
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the organization associated with this product.
    /// </summary>
    public Organization Organization { get; set; } = default!;
}