using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;

namespace Feedback.Analyzer.Application.Common.PromptCategory.Models;

/// <summary>
/// DTO for Analysis Prompt Category
/// </summary>
public class AnalysisPromptCategoryDto
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Category of the prompt
    /// </summary>
    public FeedbackAnalysisPromptCategory Category { get; set; }

    /// <summary>
    /// Display name of the category
    /// </summary>
    public string TypeDisplayName => Category.GetDisplayName();

    /// <summary>
    /// Number of prompts in the category
    /// </summary>
    public int PromptsCount { get; set; }

    /// <summary>
    /// Unique identifier of the selected prompt
    /// </summary>
    public Guid? SelectedPromptId { get; set; }
}