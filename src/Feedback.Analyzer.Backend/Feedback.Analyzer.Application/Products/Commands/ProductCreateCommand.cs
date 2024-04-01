using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Products.Commands;

/// <summary>
/// Represents a command for creating a new product.
/// </summary>
public record ProductCreateCommand : ICommand<ProductDto>
{
    /// <summary>
    /// The product data to be created.
    /// </summary>
    public ProductDto Product { get; set; } = default!;
}
