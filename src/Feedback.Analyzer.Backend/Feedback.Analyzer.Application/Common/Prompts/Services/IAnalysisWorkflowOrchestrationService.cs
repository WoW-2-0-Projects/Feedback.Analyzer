using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IAnalysisWorkflowOrchestrationService
{
    ValueTask ExecuteAsync(FeedbackAnalysisWorkflowContext feedbackAnalysisWorkflowContext, CancellationToken cancellationToken = default);
}