using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command for updating customer feedback data.
/// </summary>
public class CustomerFeedbackUpdateCommand : ICommand<CustomerFeedbackDto>
{
    public CustomerFeedbackDto CustomerFeedback { get; set; } = default!;
}
