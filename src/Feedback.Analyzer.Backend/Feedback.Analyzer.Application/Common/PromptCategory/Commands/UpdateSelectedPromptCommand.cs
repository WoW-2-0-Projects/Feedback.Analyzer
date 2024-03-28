using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Common.PromptCategory.Commands;

/// <summary>
/// Represents a command for updating the selected prompt.
/// </summary>
public class UpdateSelectedPromptCommand : ICommand< bool>
{
    /// <summary>
    /// Gets or sets the ID of the category to which the prompt belongs.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the prompt to be selected.
    /// </summary>
    public Guid PromptId { get; set; }
}
