using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Products.Queries;

/// <summary>
/// Represents a query to retrieve a product by its unique identifier.
/// </summary>
public record ProductGetByIdQuery : IQuery<ProductDto?>
{
    /// <summary>
    /// The ID of the product to be retrieved.
    /// </summary>
    public Guid ProductId { get; set; }
}
