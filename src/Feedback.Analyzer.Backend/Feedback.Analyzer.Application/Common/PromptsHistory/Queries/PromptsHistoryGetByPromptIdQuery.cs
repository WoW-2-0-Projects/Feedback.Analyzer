using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptsHistory.Queries;

/// <summary>
/// Represents prompt result get by prompt Id query
/// </summary>
public record PromptsHistoryGetByPromptIdQuery : IQuery<ICollection<PromptsExecutionHistoryDto>>
{
    /// <summary>
    /// Gets Id of prompt to get history for
    /// </summary>
    public Guid PromptId { get; init; }
}