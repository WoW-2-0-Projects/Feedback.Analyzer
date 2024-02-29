using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Queries;

public record CustomerFeedbackGetByIdQuery : IQuery<CustomerFeedbackDto?>
{
    public Guid ProductId { get; set; } = Guid.Empty;
}