using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

public class AnalyzeFeedbackEventHandler(IServiceScopeFactory serviceScopeFactory) : 
EventHandlerBase<AnalyzeFeedbackEvent>
{
    protected override async ValueTask HandleAsync(
        AnalyzeFeedbackEvent @event, 
        CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var feedbackAnalysisOrchestrationService = scopedServiceProvider.GetRequiredService<IFeedbackAnalysisOrchestrationService>();

        await feedbackAnalysisOrchestrationService.ExecuteWorkflowAsync(@event.Context, cancellationToken);
    }
}