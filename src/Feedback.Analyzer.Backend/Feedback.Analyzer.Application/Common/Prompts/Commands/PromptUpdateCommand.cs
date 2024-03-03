using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Common.Prompts.Commands;

/// <summary>
/// Represents a command to modify an existing organization's information.
/// </summary>
public record PromptUpdateCommand : ICommand<AnalysisPromptDto>
{
    /// <summary>
    /// Contains the updated organization data. 
    /// </summary>
    public AnalysisPromptDto Prompt { get; set; } = default!;
}