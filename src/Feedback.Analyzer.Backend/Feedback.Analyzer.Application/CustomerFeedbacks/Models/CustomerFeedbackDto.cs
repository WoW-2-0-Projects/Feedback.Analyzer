namespace Feedback.Analyzer.Application.CustomerFeedbacks.Models;

/// <summary>
/// Represents the data transfer object (DTO) for customer feedback.
/// </summary>
public class CustomerFeedbackDto
{
    /// <summary>
    /// Gets or sets the comment provided by the customer.
    /// </summary>
    public string Comment { get; set; } = default!;

    /// <summary>
    /// Gets or sets the username of the customer who provided the feedback.
    /// </summary>
    public string UserName { get; set; } = default!;
}
