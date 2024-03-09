using System.Collections.Immutable;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IPromptExecutionProcessingService
{
    ValueTask<IReadOnlyList<PromptExecutionHistory>> ExecuteAsync(
        AnalysisPrompt prompt,
        Dictionary<string, string> arguments,
        uint executionCount = 1,
        CancellationToken cancellationToken = default
    );
}