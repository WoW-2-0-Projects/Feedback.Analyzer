using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Entity class that represents a category of analysis prompts.
/// </summary>
public class AnalysisPromptCategory : Entity
{
    /// <summary>
    /// Gets or sets the type of analysis prompt category.
    /// </summary>
    public FeedbackAnalysisPromptCategory Category { get; set; }
    
    /// <summary>
    /// Gets or sets the type of prompt.
    /// </summary>
    public PromptType Type { get; set; }

    /// <summary>
    /// Gets or sets the ID of the selected prompt.
    /// </summary>
    public Guid? SelectedPromptId { get; set; }

    /// <summary>
    /// Gets or sets the selected prompt.
    /// </summary>
    public AnalysisPrompt? SelectedPrompt { get; set; }

    /// <summary>
    /// Gets or sets the collection of prompts.
    /// </summary>
    public ICollection<AnalysisPrompt> Prompts { get; set; } = new List<AnalysisPrompt>();
}