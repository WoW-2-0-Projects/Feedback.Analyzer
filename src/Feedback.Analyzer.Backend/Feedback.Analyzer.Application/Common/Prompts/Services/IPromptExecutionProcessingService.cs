using System.Collections.Immutable;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

/// <summary>
/// Represents a service for executing a prompt asynchronously with the provided arguments.
/// </summary>
public interface IPromptExecutionProcessingService
{
    ValueTask<IImmutableList<PromptExecutionHistory>> ExecuteAsync(
        Guid promptId,
        Dictionary<string, string> arguments,
        uint executionCount = 1,
        CancellationToken cancellationToken = default
    );
}