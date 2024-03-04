using Feedback.Analyzer.Application.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.PromptsHistory.Commands;

public record PromptExecutionHistoryCreateCommand : ICommand<PromptExecutionHistoryDto>
{
    public PromptExecutionHistoryDto PromptExecutionHistoryDto { get; set; } = default!;
}