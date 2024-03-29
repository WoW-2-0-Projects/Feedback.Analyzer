using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Queries;

/// <summary>
/// Represents a query for retrieving customer feedback by its ID.
/// </summary>
public record CustomerFeedbackGetByIdQuery : IQuery<CustomerFeedbackDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer feedback.
    /// </summary>
    public Guid ProductId { get; set; } = Guid.Empty;
}
