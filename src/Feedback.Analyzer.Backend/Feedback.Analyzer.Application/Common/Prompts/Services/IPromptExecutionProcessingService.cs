using System.Collections.Immutable;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

/// <summary>
/// Defines prompt execution processing service functionality
/// </summary>
public interface IPromptExecutionProcessingService
{
    /// <summary>
    /// Executes the prompt with the given arguments.
    /// </summary>
    /// <param name="prompt">The prompt to execute.</param>
    /// <param name="arguments">The arguments to pass to the prompt.</param>
    /// <param name="executionCount">The number of times to execute the prompt.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation, containing the prompt execution history.</returns>
    ValueTask<IImmutableList<PromptExecutionHistory>> ExecuteAsync(
        AnalysisPrompt prompt,
        Dictionary<string,string> arguments,
        uint executionCount = 1,
        CancellationToken cancellationToken = default
    );
}