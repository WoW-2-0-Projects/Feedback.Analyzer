namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents metrics related to feedback.
/// </summary>
public class FeedbackMetrics
{
    /// <summary>
    /// Gets or sets the Net Promoter Score (NPS) metric.
    /// </summary>
    public float Nps { get; set; }

    /// <summary>
    /// Gets or sets the Customer Satisfaction (CSAT) metric.
    /// </summary>
    public float Csat { get; set; }

    /// <summary>
    /// Gets or sets the Customer Effort Score (CES) metric.
    /// </summary>
    public float Ces { get; set; }
}