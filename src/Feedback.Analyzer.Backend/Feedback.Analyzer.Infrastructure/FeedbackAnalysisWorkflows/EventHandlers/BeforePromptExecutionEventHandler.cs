using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
/// Represents event handler for before prompt execution hook event
/// </summary>
public class BeforePromptExecutionEventHandler : EventHandlerBase<BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
{
    protected override ValueTask HandleAsync(BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> notification, CancellationToken cancellationToken)
    {
        if (notification.Context is not SingleFeedbackAnalysisWorkflowContext context)
            return ValueTask.CompletedTask;

        switch (notification.Prompt.Category.Category)
        {
            case FeedbackAnalysisPromptCategory.PersonalInformationRedaction:
                context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.ExtractedRelevantContent; 
                break;
            case FeedbackAnalysisPromptCategory.OpinionMining:
                context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.PiiRedactedContent;
                break;
            case FeedbackAnalysisPromptCategory.OpinionPointsExtraction:
                context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.PiiRedactedContent;
                break;
            case FeedbackAnalysisPromptCategory.QuestionPointsExtraction:
                context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.PiiRedactedContent;
                break;
        }

        return ValueTask.CompletedTask;
    }
}