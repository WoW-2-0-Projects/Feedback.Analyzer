using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Products.Commands;

/// <summary>
/// Represents a command for deleting a product by its ID.
/// </summary>
public record ProductDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// The ID of the product to be deleted.
    /// </summary>
    public Guid ProductId { get; set; }
}
