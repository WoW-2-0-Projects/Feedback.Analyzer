using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;

namespace Feedback.Analyzer.Application.Common.PromptCategories.Models;

public class AnalysisPromptCategoryDto
{
    public Guid Id { get; set; }
    
    public FeedbackAnalysisPromptCategory Type { get; set; }

    public string TypeDisplayName => Type.GetDisplayName(); 
    
    public int PromptsCount { get; set; }
    
    public Guid? SelectedPromptId { get; set; }
    
    public AnalysisPromptDto? SelectedPrompt { get; set; }
}