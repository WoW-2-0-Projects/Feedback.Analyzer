using Feedback.Analyzer.Application.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.PromptsHistory.Commands;

/// <summary>
/// Represents a command for creating a prompt execution history record.
/// </summary>
public record PromptExecutionHistoryCreateCommand : ICommand<PromptExecutionHistoryDto>
{
    /// <summary>
    /// Gets or sets the DTO representing the prompt execution history.
    /// </summary>
    public PromptExecutionHistoryDto PromptExecutionHistoryDto { get; set; } = default!;
}