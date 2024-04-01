using Feedback.Analyzer.Domain.Common.Charts;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents key and common opinion point chosen among feedback analysis results 
/// </summary>
public class FeedbackAnalysisWorkflowResultPoint
{
    /// <summary>
    /// Gets or sets related feedback result id
    /// </summary>
    public Guid FeedbackResultId { get; set; }

    /// <summary>
    /// Gets or sets related feedback analysis workflow result id
    /// </summary>
    public Guid WorkflowResultId { get; set; }

    /// <summary>
    /// Gets or sets point type
    /// </summary>
    public AnalysisResultType Type { get; set; }

    /// <summary>
    /// Gets or sets point
    /// </summary>
    public string Point { get; set; } = default!;
}