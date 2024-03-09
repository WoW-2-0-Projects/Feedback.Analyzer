using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
///  Represents event handler for before prompt execution hook event
/// </summary>
public class AfterPromptExecutionEventHandler : IEventHandler<BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
{
    public Task Handle(BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}