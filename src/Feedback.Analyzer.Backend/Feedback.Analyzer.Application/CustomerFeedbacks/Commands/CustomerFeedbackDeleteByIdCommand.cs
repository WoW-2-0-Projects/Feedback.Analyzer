using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command for deleting customer feedback by product ID.
/// </summary>
public class CustomerFeedbackDeleteByIdCommand : ICommand<bool>
{
    public Guid ProductId { get; set; } 
}