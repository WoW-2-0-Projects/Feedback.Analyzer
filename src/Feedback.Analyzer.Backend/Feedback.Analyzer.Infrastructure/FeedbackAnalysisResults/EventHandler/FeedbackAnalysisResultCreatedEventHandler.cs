using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Event;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.EventHandler;

/// <summary>
/// Represents feedback analysis result created event handler
/// </summary>
public class FeedbackAnalysisResultCreatedEventHandler(
    ICacheBroker cacheBroker,
    IEventBusBroker eventBusBroker,
    IFeedbackAnalysisWorkflowResultService feedbackAnalysisWorkflowResultService
) : EventHandlerBase<FeedbackAnalysisResultCreatedEvent>
{
    protected override async ValueTask HandleAsync(FeedbackAnalysisResultCreatedEvent @event, CancellationToken cancellationToken)
    {
        // Get feedback analysis workflow result progress from cache
        var workflowResultProgress = await cacheBroker.GetOrSetAsync(
            $"{@event.FeedbackAnalysisResult.FeedbackAnalysisWorkflowResultId}-{CacheConstants.WorkflowResultCacheKey}",
            async () =>
            {
                if (!await cacheBroker.TryGetAsync(
                        @event.FeedbackAnalysisResult.FeedbackAnalysisWorkflowResultId.ToString(),
                        out FeedbackAnalysisWorkflowResult? workflowResult,
                        cancellationToken
                    ) || workflowResult is null)
                    throw new InvalidOperationException("Failed to update analyzed feedbacks count, workflow result isn't set in cache");

                return new FeedbackAnalysisWorkflowResultProgress(workflowResult);
            },
            cancellationToken: cancellationToken
        );

        // Increment count of analysed feedbacks
        workflowResultProgress.AnalyzedFeedbacksCount++;

        // Check if all feedbacks have been analysed
        if (workflowResultProgress.FeedbacksCount == workflowResultProgress.AnalyzedFeedbacksCount)
        {
            // Remove workflow result progress from cache
            await cacheBroker.DeleteAsync(
                $"{@event.FeedbackAnalysisResult.FeedbackAnalysisWorkflowResultId}-{CacheConstants.WorkflowResultCacheKey}",
                cancellationToken: cancellationToken
            );

            // Publish feedback analysis workflow result update event
            await eventBusBroker.PublishAsync(
                new FeedbackAnalysisWorkflowResultUpdateEvent(workflowResultProgress.WorkflowResultId),
                cancellationToken
            );
        }
        else
            await cacheBroker.SetAsync(
                $"{@event.FeedbackAnalysisResult.FeedbackAnalysisWorkflowResultId}-{CacheConstants.WorkflowResultCacheKey}",
                workflowResultProgress,
                cancellationToken: cancellationToken
            );
    }
}