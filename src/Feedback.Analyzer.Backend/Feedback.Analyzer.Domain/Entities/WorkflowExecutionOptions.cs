using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class WorkflowExecutionOptions : Entity, IParameterizedCloneable<FeedbackExecutionWorkflow, WorkflowExecutionOptions>
{
    public Guid AnalysisPromptCategoryId { get; set; }

    // public Guid FeedbackExecutionWorkflowId { get; set; }

    public AnalysisPromptCategory AnalysisPromptCategory { get; set; } = default!;

    public FeedbackExecutionWorkflow FeedbackExecutionWorkflow { get; set; } = default!;
    
    public Guid? ParentId { get; set; }
    
    public bool IsDisabled { get; set; }

    public List<WorkflowExecutionOptions> ChildExecutionOptions { get; set; } = default!;

    public WorkflowExecutionOptions Clone(FeedbackExecutionWorkflow args)
    {
        return new WorkflowExecutionOptions
        {
            AnalysisPromptCategoryId = AnalysisPromptCategoryId,
            FeedbackExecutionWorkflow = args,
            ChildExecutionOptions = ChildExecutionOptions?.Select(options => options.Clone(args)).ToList()
        };
    }
}