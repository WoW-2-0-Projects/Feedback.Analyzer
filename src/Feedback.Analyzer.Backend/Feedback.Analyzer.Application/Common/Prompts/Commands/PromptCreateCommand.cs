using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Common.Prompts.Commands;

/// <summary>
/// Represents a command to create a new organization. 
/// </summary>
public record PromptCreateCommand : ICommand<AnalysisPromptDto>
{
    /// <summary>
    /// The data required to create a new organization.
    /// </summary>
    public AnalysisPromptDto Prompt { get; set; } = default!; 
}