using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

public class AnalyzeFeedbackEventHandler(IServiceScopeFactory serviceScopeFactory) : IEventHandler<AnalyzeFeedbackEventBase>
{
    public async Task Handle(AnalyzeFeedbackEventBase notification, CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var feedbackAnalysisOrchestrationService = scopedServiceProvider.GetRequiredService<IFeedbackAnalysisOrchestrationService>();

        await feedbackAnalysisOrchestrationService.ExecuteWorkflowAsync(notification.Context, cancellationToken);
    }

    public Task Consume(ConsumeContext<AnalyzeFeedbackEventBase> context)
    {
        throw new NotImplementedException();
    }
}