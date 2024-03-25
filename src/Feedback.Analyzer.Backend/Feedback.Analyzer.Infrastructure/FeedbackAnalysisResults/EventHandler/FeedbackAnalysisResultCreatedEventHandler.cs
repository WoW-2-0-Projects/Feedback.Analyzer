using Feedback.Analyzer.Application.FeedbackAnalysisResults.Event;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Infrastructure.Common.EventBus.Brokers;
using Feedback.Analyzer.Persistence.Caching.Brokers;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.EventHandler;

/// <summary>
/// Represents feedback analysis result created event handler
/// </summary>
public class FeedbackAnalysisResultCreatedEventHandler(
    ICacheBroker cacheBroker,
    MassTransitEventBusBroker massTransitEventBusBroker,
    IFeedbackAnalysisWorkflowResultService feedbackAnalysisWorkflowResultService) :
    EventHandlerBase<FeedbackAnalysisResultCreatedEvent>
{
    protected override async ValueTask HandleAsync(FeedbackAnalysisResultCreatedEvent @event, CancellationToken cancellationToken)
    {
        cacheBroker.SetAsync(@event.FeedbackAnalysisResults.Id.ToString(), @event.FeedbackAnalysisResults);

        var workflowResult =await cacheBroker.GetAsync<FeedbackAnalysisWorkflowResult
            >(@event.Id.ToString(), cancellationToken);

        var countOfAnalysedFeedbacks = workflowResult.FeedbackAnalysisResults.Count();
        
        if (Convert.ToUInt32(countOfAnalysedFeedbacks) == workflowResult.FeedbacksCount)
        {
            await massTransitEventBusBroker.PublishLocalAsync(new FeedbackAnalysisResultUpdateEvent { WorkflowResult = workflowResult });
        }
    }
}
