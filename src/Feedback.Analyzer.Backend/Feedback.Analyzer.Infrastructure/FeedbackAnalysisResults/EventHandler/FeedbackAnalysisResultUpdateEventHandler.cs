using Feedback.Analyzer.Application.FeedbackAnalysisResults.Event;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.EventHandler;

/// <summary>
/// Represents feedback analysis result update event handler
/// </summary>
public class FeedbackAnalysisResultUpdateEventHandler(
    IFeedbackAnalysisWorkflowResultService feedbackAnalysisWorkflowResultService) : EventHandlerBase<FeedbackAnalysisResultUpdateEvent>
{
    protected override async ValueTask HandleAsync(FeedbackAnalysisResultUpdateEvent @event, CancellationToken cancellationToken)
    {
        await feedbackAnalysisWorkflowResultService.UpdateAsync(@event.WorkflowResult, cancellationToken: cancellationToken);
    }
}
