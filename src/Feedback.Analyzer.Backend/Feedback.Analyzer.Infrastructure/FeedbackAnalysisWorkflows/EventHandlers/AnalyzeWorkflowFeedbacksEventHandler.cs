using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

public class AnalyzeWorkflowFeedbacksEventHandler(IServiceScopeFactory serviceScopeFactory) : IEventHandler<AnalyzeWorkflowFeedbacksEvent>
{
    public async Task Handle(AnalyzeWorkflowFeedbacksEvent notification, CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var feedbackBatchAnalysisOrchestrationService = scopedServiceProvider.GetRequiredService<IFeedbackBatchAnalysisOrchestrationService>();

        await feedbackBatchAnalysisOrchestrationService.RunWorkflowAsync(notification.WorkflowId, cancellationToken);
    }
}