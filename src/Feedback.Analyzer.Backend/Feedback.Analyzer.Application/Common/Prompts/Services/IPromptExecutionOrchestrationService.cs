using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IPromptExecutionOrchestrationService
{
    ValueTask ExecuteAsync(WorkflowExecutionOptions headPromptOption, CustomerFeedback feedback,
        CancellationToken cancellationToken = default);
}