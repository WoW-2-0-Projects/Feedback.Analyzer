using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class AnalysisPromptCategory : Entity
{
    public FeedbackAnalysisPromptCategory Category { get; set; }
    
    public PromptType Type { get; set; }
    
    public Guid? SelectedPromptId { get; set; }
    
    public AnalysisPrompt? SelectedPrompt { get; set; }
    
    public ICollection<AnalysisPrompt> Prompts { get; set; } = new List<AnalysisPrompt>();
    
    public List<WorkflowExecutionOptions> FeedbackWorkflowExecutionOptions { get; set; }
}