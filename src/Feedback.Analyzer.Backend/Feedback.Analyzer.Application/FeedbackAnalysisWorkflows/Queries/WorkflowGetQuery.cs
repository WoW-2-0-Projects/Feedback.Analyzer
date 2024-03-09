using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;

public record WorkflowGetQuery : IQuery<ICollection<FeedbackExecutionWorkflowDto>>
{
    public FeedbackExecutionWorkflowFilter Filter { get; set; } = default!;
}