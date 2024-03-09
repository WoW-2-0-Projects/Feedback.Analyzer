using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackExecutionWorkflow : AuditableEntity, ICloneable<FeedbackExecutionWorkflow>
{
    public string Name { get; set; } = default!;
    
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public WorkflowType Type { get; set; }

    public Guid StartingExecutionOptionId { get; set; }
    
    public WorkflowExecutionOptions StartingExecutionOption { get; set; } = default!;

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