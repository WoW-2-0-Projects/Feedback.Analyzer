using System.ComponentModel.DataAnnotations.Schema;
using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents a customer feedback entity with audit information.
/// </summary>
public class CustomerFeedback : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the product associated with this feedback.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the comment provided in the feedback.
    /// </summary>
    public string Comment { get; set; } = default!;

    /// <summary>
    /// Gets or sets the username associated with the feedback.
    /// </summary>
    public string UserName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product associated with this feedback.
    /// </summary>
    public Product Product { get; set; } = default!;

    /// <summary>
    /// Gets or sets the analysis result of the feedback.
    /// </summary>
    public ICollection<FeedbackAnalysisResult> FeedbackAnalysisResult { get; set; } = default!;
}
