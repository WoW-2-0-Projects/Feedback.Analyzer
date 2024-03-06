using Feedback.Analyzer.Application.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.PromptsHistory.Queries;

/// <summary>
/// Represents a query for retrieving prompt execution history records by prompt ID.
/// </summary>
public record PromptExecutionHistoryGetByPromptIdQuery : IQuery<IList<PromptExecutionHistoryDto>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the prompt.
    /// </summary>
    public Guid PromptId { get; set; } = default!;
}