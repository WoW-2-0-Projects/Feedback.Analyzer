using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class WorkflowExecutionOption : Entity
{
    public Guid AnalysisPromptCategoryId { get; set; }

    public AnalysisPromptCategory AnalysisPromptCategory { get; set; } = default!;
    
    public Guid WorkflowId { get; set; }

    public AnalysisWorkflow Workflow { get; set; } = default!;
    
    public bool IsDisabled { get; set; }
    
    public Guid? ParentId { get; set; }

    public IReadOnlyList<WorkflowExecutionOption>? ChildExecutionOptions { get; set; }
}