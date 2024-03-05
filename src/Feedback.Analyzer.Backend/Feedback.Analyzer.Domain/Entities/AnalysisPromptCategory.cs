using System.ComponentModel.DataAnnotations.Schema;
using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class AnalysisPromptCategory : Entity
{
    public FeedbackAnalysisPromptType Type { get; set; }
    
    public Guid? SelectedPromptId { get; set; }
    
    public AnalysisPrompt? SelectedPrompt { get; set; }
    
    public ICollection<AnalysisPrompt> Prompts { get; set; } = new List<AnalysisPrompt>();
    
    public List<WorkflowPromptCategoryExecutionOptions> FeedbackWorkflowExecutionOptions { get; set; }
}