using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;

/// <summary>
/// Represents the DTO (Data Transfer Object) for feedback analysis results.
/// </summary>
public record FeedbackAnalysisResultDto
{
    /// <summary>
    /// Gets or sets the feedback analysis result Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the feedback is relevant.
    /// </summary>
    public bool IsRelevant { get; set; }

    /// <summary>
    /// Gets or sets the overall opinion type of the feedback.
    /// </summary>
    public OpinionType Opinion { get; set; } = OpinionType.Neutral;

    /// <summary>
    /// Gets or sets the status of the analysis.
    /// </summary>
    public WorkflowStatus Status { get; set; }

    /// <summary>
    /// Gets or sets an array of recognized languages in the feedback content.
    /// </summary>
    public string[]? Languages { get; set; }

    /// <summary>
    /// Gets or sets an array of positive points mentioned in the feedback.
    /// </summary>
    public string[]? PositiveOpinionPoints { get; set; }

    /// <summary>
    /// Gets or sets an array of negative points mentioned in the feedback.
    /// </summary>
    public string[]? NegativeOpinionPoints { get; set; }

    /// <summary>
    /// Gets or sets the analysis time in milliseconds.
    /// </summary>
    public ulong ModelExecutionDurationInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the analysis time in milliseconds.
    /// </summary>
    public ulong AnalysisDurationInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the customer feedback associated with this analysis result.
    /// </summary>
    public CustomerFeedbackDto CustomerFeedback { get; set; } = default!;
}