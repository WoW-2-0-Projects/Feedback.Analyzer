using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Query;

/// <summary>
/// Represents feedback workflow result get by workflow Id query
/// </summary>
public class FeedbackWorkflowResultGetQuery : IQuery<ICollection<FeedbackAnalysisWorkflowResultDto>>
{
    /// <summary>
    /// Gets or sets workflow results filter
    /// </summary>
    public FeedbackAnalysisWorkflowResultFilter Filter { get; set; }
}