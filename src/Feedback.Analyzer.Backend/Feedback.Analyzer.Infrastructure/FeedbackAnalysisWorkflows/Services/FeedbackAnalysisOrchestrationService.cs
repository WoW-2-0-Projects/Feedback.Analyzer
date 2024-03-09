using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

public class FeedbackAnalysisOrchestrationService : IFeedbackAnalysisOrchestrationService
{
    public ValueTask ExecuteWorkflowAsync(SingleFeedbackAnalysisWorkflowContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}