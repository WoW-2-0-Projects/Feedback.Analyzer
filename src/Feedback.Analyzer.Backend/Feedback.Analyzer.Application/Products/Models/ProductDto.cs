using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Products.Models;

/// <summary>
/// Represents a data transfer object (DTO) for a product.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the product.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// The description of the product.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// The ID of the organization the product belongs to.
    /// </summary>
    public Guid OrganizationId { get; set; }
}
