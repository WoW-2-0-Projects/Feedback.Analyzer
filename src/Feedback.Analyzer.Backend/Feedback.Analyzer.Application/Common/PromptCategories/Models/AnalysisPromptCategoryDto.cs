using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.PromptCategories.Models;

public class AnalysisPromptCategoryDto
{
    public Guid Id { get; set; }
    
    public FeedbackAnalysisPromptType Type { get; set; }

    public string TypeDisplayName { get; set; } = string.Empty;
    
    public Guid SelectedPromptId { get; set; }
}