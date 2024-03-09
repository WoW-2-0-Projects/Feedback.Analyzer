using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

public class AnalyzeFeedbackEventHandler : IEventHandler<AnalyzeFeedbackEvent>
{
    public Task Handle(AnalyzeFeedbackEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}