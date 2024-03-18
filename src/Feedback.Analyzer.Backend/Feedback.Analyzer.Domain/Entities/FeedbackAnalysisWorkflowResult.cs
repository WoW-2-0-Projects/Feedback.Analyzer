using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents the result of a feedback analysis workflow.
/// </summary>
public class FeedbackAnalysisWorkflowResult : Entity
{
    /// <summary>
    /// Gets or sets the ID of the associated workflow.
    /// </summary>
    public Guid WorkflowId { get; set; }
    
    /// <summary>
    /// Gets or sets the count of feedbacks analyzed in the workflow.
    /// </summary>
    public ulong FeedbacksCount { get; set; }
    
    /// <summary>
    /// Gets or sets the start time of the workflow.
    /// </summary>
    public DateTimeOffset StartedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the finish time of the workflow.
    /// </summary>
    public DateTimeOffset FinishedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the associated feedback analysis workflow.
    /// </summary>
    public FeedbackAnalysisWorkflow Workflow { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of feedback analysis results associated with this workflow result.
    /// </summary>
    public ICollection<FeedbackAnalysisResult> FeedbackAnalysisResults { get; set; } = default!;
}