using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;

/// <summary>
/// Represents feedback analysis event
/// </summary>
/// <param name="Context">Feedback analysis context</param>
public record AnalyzeFeedbackEvent(SingleFeedbackAnalysisWorkflowContext Context) : EventBase;