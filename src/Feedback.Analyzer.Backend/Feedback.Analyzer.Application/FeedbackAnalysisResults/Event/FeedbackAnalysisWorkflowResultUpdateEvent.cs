using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Event;

/// <summary>
/// Represents feedback analysis workflow result update event
/// </summary>
/// <param name="WorkflowResultId">Workflow result id to update</param>
public record FeedbackAnalysisWorkflowResultUpdateEvent(Guid WorkflowResultId) : EventBase;