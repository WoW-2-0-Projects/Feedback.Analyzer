using Feedback.Analyzer.Application.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.PromptsHistory.Queries;

public record PromptExecutionHistoryGetByPromptIdQuery : IQuery<IList<PromptExecutionHistoryDto>>
{
    public Guid PromptId { get; set; } = default!;
}