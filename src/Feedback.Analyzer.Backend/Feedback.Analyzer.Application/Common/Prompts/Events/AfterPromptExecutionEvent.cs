using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.Prompts.Events;

/// <summary>
/// Represents the prompt execution hook event that is triggered after the prompt execution.
/// </summary>
/// <typeparam name="TWorkflow"></typeparam>
public record AfterPromptExecutionEvent<TWorkflow> : Event where TWorkflow : WorkflowContext
{
    /// <summary>
    /// Execution context
    /// </summary>
    public WorkflowContext Context { get; set; } = default!;
}