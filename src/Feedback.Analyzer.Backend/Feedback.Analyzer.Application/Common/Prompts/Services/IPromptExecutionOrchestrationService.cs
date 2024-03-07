using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IPromptExecutionOrchestrationService
{
    ValueTask ExecuteAsync(WorkflowPromptCategoryExecutionOptions headPromptOption, CustomerFeedback feedback,
        CancellationToken cancellationToken = default);
}