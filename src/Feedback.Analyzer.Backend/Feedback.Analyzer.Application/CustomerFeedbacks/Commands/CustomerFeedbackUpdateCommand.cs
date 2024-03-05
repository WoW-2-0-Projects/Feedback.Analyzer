using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Commands;

/// <summary>
/// Represents a command for updating customer feedback data.
/// </summary>
public record CustomerFeedbackUpdateCommand : ICommand<CustomerFeedbackDto>
{
    public CustomerFeedbackDto CustomerFeedback { get; set; } = default!;
}
