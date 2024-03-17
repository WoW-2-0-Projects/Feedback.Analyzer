using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Modal;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Query;

/// <summary>
/// Represents feedback workflow result get by workflow Id query
/// </summary>
public class FeedbackWorkflowResultGetByWorkflowIdQuery : IQuery<ICollection<FeedbackAnalysisWorkflowResultDto>>
{
    /// <summary>
    /// Gets or sets workflow Id
    /// </summary>
    public Guid WorkflowId { get; set; }
}