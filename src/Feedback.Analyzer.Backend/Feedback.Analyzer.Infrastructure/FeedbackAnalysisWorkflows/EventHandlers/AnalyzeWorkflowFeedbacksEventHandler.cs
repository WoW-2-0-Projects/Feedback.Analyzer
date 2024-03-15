using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
/// Represents workflow execution event handler
/// </summary>
public class AnalyzeWorkflowFeedbacksEventHandler(IServiceScopeFactory serviceScopeFactory) : IEventHandler<AnalyzeWorkflowFeedbacksEventBase>
{
    public async Task Handle(AnalyzeWorkflowFeedbacksEventBase notification, CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var feedbackBatchAnalysisOrchestrationService = scopedServiceProvider.GetRequiredService<IFeedbackBatchAnalysisOrchestrationService>();

        await feedbackBatchAnalysisOrchestrationService.RunWorkflowAsync(notification.WorkflowId, cancellationToken);
    }

    public Task Consume(ConsumeContext<AnalyzeWorkflowFeedbacksEventBase> context)
    {
        throw new NotImplementedException();
    }
}