using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Clients.Queries;

public record CustomerFeedbackGetByIdQuery : IQuery<CustomerFeedbackDto?>
{
    public Guid ProductId { get; set; }
}