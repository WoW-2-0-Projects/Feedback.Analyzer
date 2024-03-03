using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Products.Queries;

/// <summary>
/// Represents a query to retrieve a collection of products with optional filtering.
/// </summary>
public record ProductGetQuery : IQuery<ICollection<ProductDto>>
{
    /// <summary>
    /// Optional filter criteria for retrieving products.
    /// </summary>
    public ProductFilter Filter { get; set; } = default!;
}
