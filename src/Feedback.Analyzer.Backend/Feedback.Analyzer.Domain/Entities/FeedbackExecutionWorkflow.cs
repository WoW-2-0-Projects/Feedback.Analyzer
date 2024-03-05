using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackExecutionWorkflow : AuditableEntity, ICloneable<FeedbackExecutionWorkflow>
{
    public Guid ProductId { get; set; }

    public WorkflowType Type { get; set; }

    public List<WorkflowPromptCategoryExecutionOptions> FeedbackWorkflowExecutionOptions { get; set; } = default!;

    public FeedbackExecutionWorkflow Clone()
    {
        return new FeedbackExecutionWorkflow
        {
            Type = Type,
            FeedbackWorkflowExecutionOptions = FeedbackWorkflowExecutionOptions.Select(options => options.Clone(this)).ToList()
        };
    }
}