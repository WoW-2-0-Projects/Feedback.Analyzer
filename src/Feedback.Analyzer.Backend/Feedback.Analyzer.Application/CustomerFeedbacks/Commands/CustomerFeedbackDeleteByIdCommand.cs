using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Commands;

/// <summary>
/// Represents a command for deleting customer feedback by its ID.
/// </summary>
public record CustomerFeedbackDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer feedback to delete.
    /// </summary>
    public Guid ProductId { get; set; }
}
