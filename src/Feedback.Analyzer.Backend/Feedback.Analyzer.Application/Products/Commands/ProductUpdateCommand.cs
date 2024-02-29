using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Products.Commands;

/// <summary>
/// Represents a command for updating an existing product.
/// </summary>
public record ProductUpdateCommand : ICommand<ProductDto>
{
    /// <summary>
    /// The product data containing the updated values.
    /// </summary>
    public ProductDto Product { get; set; } = default!;
}
