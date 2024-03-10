using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Queries;

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