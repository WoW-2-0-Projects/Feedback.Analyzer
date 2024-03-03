using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;

namespace Feedback.Analyzer.Application.Common.PromptCategories.Models;

public class AnalysisPromptCategoryDto
{
    public Guid Id { get; set; }
    
    public FeedbackAnalysisPromptType Type { get; set; }

    public string TypeDisplayName => Type.GetDisplayName(); 
    
    public int PromptsCount { get; set; }
    
    public Guid SelectedPromptId { get; set; }
}