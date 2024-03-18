using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Events;

/// <summary>
/// Represents the prompt execution hook event that is triggered after the prompt execution.
/// </summary>
/// <typeparam name="TWorkflow"></typeparam>
public class AfterPromptExecutionEvent<TWorkflow> : Event where TWorkflow : WorkflowContext
{
    /// <summary>
    /// Gets execution context
    /// </summary>
    public WorkflowContext Context { get; init; } = default!;

    /// <summary>
    /// Gets prompt that was executed
    /// </summary>
    public AnalysisPrompt Prompt { get; init; } = default!;
}