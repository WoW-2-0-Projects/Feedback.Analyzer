using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
/// Represents workflow execution event handler
/// </summary>
public class AnalyzeWorkflowFeedbacksEventHandler(IServiceScopeFactory serviceScopeFactory) : EventHandlerBase<AnalyzeWorkflowFeedbacksEvent>
{
    protected override async ValueTask HandleAsync(AnalyzeWorkflowFeedbacksEvent @event, CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var feedbackBatchAnalysisOrchestrationService = scopedServiceProvider.GetRequiredService<IFeedbackBatchAnalysisOrchestrationService>();

        await feedbackBatchAnalysisOrchestrationService.RunWorkflowAsync(@event.WorkflowId, cancellationToken);
    }
}