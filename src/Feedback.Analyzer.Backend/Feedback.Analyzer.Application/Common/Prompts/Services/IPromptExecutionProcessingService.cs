using System.Collections.Immutable;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IPromptExecutionProcessingService
{
    ValueTask<IImmutableList<PromptExecutionHistory>> ExecuteAsync(
        Guid promptId,
        Dictionary<string, string> arguments,
        uint executionCount = 1,
        CancellationToken cancellationToken = default
    );
}