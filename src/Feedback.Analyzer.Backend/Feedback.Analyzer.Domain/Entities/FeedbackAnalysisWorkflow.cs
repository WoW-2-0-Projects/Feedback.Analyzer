using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackAnalysisWorkflow : AuditableEntity, ICloneable<FeedbackAnalysisWorkflow>
{
    public string Name { get; set; } = default!;
    
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public WorkflowType Type { get; set; }

    public Guid EntryExecutionOptionId { get; set; }
   
    public WorkflowExecutionOptions EntryExecutionOption { get; set; } = default!;
    
    public ICollection<FeedbackAnalysisWorkflowResult> Results { get; set; } = default!;
    
    public FeedbackAnalysisWorkflow Clone()
    {
        // TODO : fix this clone logic
        var clonedWorkflow = new FeedbackAnalysisWorkflow
        {
            Type = Type,
        };

        clonedWorkflow.EntryExecutionOption = EntryExecutionOption.Clone(clonedWorkflow);

        return clonedWorkflow;
    }
}