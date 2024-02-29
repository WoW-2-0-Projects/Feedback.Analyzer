using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Commands;

/// <summary>
/// Represents a command for deleting customer feedback by product ID.
/// </summary>
public record CustomerFeedbackDeleteByIdCommand : ICommand<bool>
{
    public Guid ProductId { get; set; }
}