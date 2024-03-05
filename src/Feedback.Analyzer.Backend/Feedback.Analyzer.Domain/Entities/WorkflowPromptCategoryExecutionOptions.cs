using System.ComponentModel.DataAnnotations.Schema;
using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class WorkflowPromptCategoryExecutionOptions : Entity
{
    public Guid AnalysisPromptCategoryId { get; set; }

    public Guid FeedbackExecutionWorkflowId { get; set; }
    
    public AnalysisPromptCategory AnalysisPromptCategory { get; set; } = default!;
    
    public FeedbackExecutionWorkflow FeedbackExecutionWorkflow { get; set; } = default!;
}