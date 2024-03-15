using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

public class AnalyzeFeedbackEventHandler(IServiceScopeFactory serviceScopeFactory) : IEventConsumer<AnalyzeFeedbackEvent>
{
    public async Task Handle(AnalyzeFeedbackEvent notification, CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var feedbackAnalysisOrchestrationService = scopedServiceProvider.GetRequiredService<IFeedbackAnalysisOrchestrationService>();

        await feedbackAnalysisOrchestrationService.ExecuteWorkflowAsync(notification.Context, cancellationToken);
    }
}