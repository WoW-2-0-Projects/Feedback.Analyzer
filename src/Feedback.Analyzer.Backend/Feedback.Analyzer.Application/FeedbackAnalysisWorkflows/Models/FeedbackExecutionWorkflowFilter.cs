using Feedback.Analyzer.Domain.Common.Query;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

public class FeedbackExecutionWorkflowFilter : FilterPagination
{
    public WorkflowType Type { get; set; }
}