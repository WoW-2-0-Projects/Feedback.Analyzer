using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;

namespace Feedback.Analyzer.Application.Common.PromptCategories.Models;

public class AnalysisPromptCategoryDto
{
    public Guid Id { get; set; }
    
    public FeedbackAnalysisPromptCategory Category { get; set; }

    public string TypeDisplayName => Category.GetDisplayName(); 
    
    public int PromptsCount { get; set; }
    
    public Guid? SelectedPromptId { get; set; }

    public string? SelectedPromptVersion { get; set; } = string.Empty;
    
    // public AnalysisPromptDto? SelectedPrompt { get; set; }
}