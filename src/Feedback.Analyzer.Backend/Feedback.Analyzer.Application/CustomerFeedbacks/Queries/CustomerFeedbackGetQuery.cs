using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Queries;

/// <summary>
/// Represents a query for retrieving a collection of customer feedback data transfer objects.
/// </summary>
public record CustomerFeedbackGetQuery : IQuery<ICollection<CustomerFeedbackDto>>
{
    /// <summary>
    /// Gets the filter to be applied for retrieving customer feedback.
    /// </summary>
    public CustomerFeedbackFilter CustomerFeedbackFilter { get; set; } = default!;
}
