using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackExecutionWorkflow : AuditableEntity, ICloneable<FeedbackExecutionWorkflow>
{
    public Guid ProductId { get; set; }

    public WorkflowType Type { get; set; }

    // public List<WorkflowPromptCategoryExecutionOptions> FeedbackWorkflowExecutionOptions { get; set; } = default!;

    public Guid StartingExecutionOptionId { get; set; }
    
    public WorkflowPromptCategoryExecutionOptions StartingExecutionOption { get; set; } = default!;

    public FeedbackExecutionWorkflow Clone()
    {
        var clonedWorkflow = new FeedbackExecutionWorkflow
        {
            Type = Type,
        };

        clonedWorkflow.StartingExecutionOption = StartingExecutionOption.Clone(clonedWorkflow);

        return clonedWorkflow;
    }
}