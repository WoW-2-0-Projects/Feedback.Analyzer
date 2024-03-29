using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Commands;

/// <summary>
/// Represents a command for creating customer feedback.
/// </summary>
public record CustomerFeedbackCreateCommand : ICommand<CustomerFeedbackDto>
{
    /// <summary>
    /// Gets or sets the customer feedback data to be created.
    /// </summary>
    public CustomerFeedbackDto CustomerFeedback { get; set; } = default!;
}