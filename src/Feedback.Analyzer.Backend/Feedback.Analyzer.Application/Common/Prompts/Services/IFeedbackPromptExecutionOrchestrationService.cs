using Feedback.Analyzer.Application.Common.Prompts.Models;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IFeedbackPromptExecutionOrchestrationService
{
    ValueTask ExecuteAsync(FeedbackExecutionContext feedbackExecutionContext, CancellationToken cancellationToken = default);
}