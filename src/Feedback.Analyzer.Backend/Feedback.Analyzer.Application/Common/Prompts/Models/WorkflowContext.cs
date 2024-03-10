using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

/// <summary>
/// Represents the context of a prompt execution.
/// </summary>
public record WorkflowContext
{
    /// <summary>
    /// Workflow analysis Id
    /// </summary>
    public Guid WorkflowId { get; init; }

    /// <summary>
    /// Gets 
    /// </summary>
    public WorkflowExecutionOption EntryExecutionOption { get; set; } = default!;

    /// <summary>
    /// Gets or sets execution arguments
    /// </summary>
    public Dictionary<string, string> Arguments { get; set; } = new();

    /// <summary>
    /// Gets execution histories grouped by prompt category Id
    /// </summary>
    public IList<IGrouping<Guid, PromptExecutionHistory>> Histories { get; init; } =
        Enumerable.Empty<PromptExecutionHistory>().GroupBy(x => x.Id).ToList();

    /// <summary>
    /// Gets or sets indicator that shows the workflow status
    /// </summary>
    public WorkflowStatus Status { get; set; }

    /// <summary>
    /// Gets or sets error message
    /// </summary>
    public string? ErrorMessage { get; set; } = null;

    /// <summary>
    /// Retrieves last history for given category Id
    /// </summary>
    /// <param name="categoryId">Category Id to search histories</param>
    /// <returns>Prompt execution history</returns>
    public PromptExecutionHistory? GetLastHistory(Guid categoryId)
    {
        return Histories.First(groupedHistory => groupedHistory.Key == categoryId).MaxBy(t => t.ExecutionTime);
    }
}