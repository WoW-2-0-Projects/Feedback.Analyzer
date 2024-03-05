using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class WorkflowPromptCategoryExecutionOptions : Entity, IParameterizedCloneable<FeedbackExecutionWorkflow, WorkflowPromptCategoryExecutionOptions>
{
    public Guid AnalysisPromptCategoryId { get; set; }

    public Guid FeedbackExecutionWorkflowId { get; set; }

    public AnalysisPromptCategory AnalysisPromptCategory { get; set; } = default!;

    public FeedbackExecutionWorkflow FeedbackExecutionWorkflow { get; set; } = default!;
    
    public Guid? ParentId { get; set; }

    public List<WorkflowPromptCategoryExecutionOptions> ChildExecutionOptions { get; set; } = default!;

    public WorkflowPromptCategoryExecutionOptions Clone(FeedbackExecutionWorkflow args)
    {
        return new WorkflowPromptCategoryExecutionOptions
        {
            AnalysisPromptCategoryId = AnalysisPromptCategoryId,
            FeedbackExecutionWorkflow = args,
            ChildExecutionOptions = ChildExecutionOptions.Select(options => options.Clone(args)).ToList()
        };
    }
}