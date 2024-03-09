using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class WorkflowExecutionOptions : Entity, IParameterizedCloneable<FeedbackAnalysisWorkflow, WorkflowExecutionOptions>
{
    public Guid AnalysisPromptCategoryId { get; set; }

    // public Guid FeedbackExecutionWorkflowId { get; set; }

    public AnalysisPromptCategory AnalysisPromptCategory { get; set; } = default!;

    public FeedbackAnalysisWorkflow FeedbackAnalysisWorkflow { get; set; } = default!;
    
    public Guid? ParentId { get; set; }
    
    public bool IsDisabled { get; set; }

    public List<WorkflowExecutionOptions>? ChildExecutionOptions { get; set; }

    public WorkflowExecutionOptions Clone(FeedbackAnalysisWorkflow args)
    {
        return new WorkflowExecutionOptions
        {
            AnalysisPromptCategoryId = AnalysisPromptCategoryId,
            FeedbackAnalysisWorkflow = args,
            ChildExecutionOptions = ChildExecutionOptions?.Select(options => options.Clone(args)).ToList()
        };
    }
}