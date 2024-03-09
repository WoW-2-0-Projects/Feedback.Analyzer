using Feedback.Analyzer.Domain.Common.Exceptions;
using Microsoft.SemanticKernel;

namespace Feedback.Analyzer.Application.Common.Prompts.Brokers;

/// <summary>
/// Interface for prompt execution broker
/// </summary>
public interface IPromptExecutionBroker
{
    /// <summary>
    /// Executes the prompt
    /// </summary>
    /// <param name="prompt">The prompt to execute</param>
    /// <param name="arguments">The kernel arguments</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the prompt execution</returns>
    ValueTask<FuncResult<string?>> ExecutePromptAsync(string prompt, KernelArguments arguments, CancellationToken cancellationToken = default);
}