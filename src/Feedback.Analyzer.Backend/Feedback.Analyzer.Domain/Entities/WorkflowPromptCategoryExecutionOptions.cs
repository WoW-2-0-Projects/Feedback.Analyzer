using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class WorkflowPromptCategoryExecutionOptions : Entity
{
    public Guid AnalysisPromptCategoryId { get; set; }

    public Guid FeedbackExecutionWorkflowId { get; set; }
}