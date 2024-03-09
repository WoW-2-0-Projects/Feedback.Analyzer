using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IWorkflowExecutionService
{
    ValueTask ExecuteAsync(WorkflowExecutionOption headPromptOption, CustomerFeedback feedback,
        CancellationToken cancellationToken = default);
}