using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
///  Represents event handler for before prompt execution hook event
/// </summary>
public class AfterPromptExecutionEventHandler : IEventHandler<AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
{
    public Task Handle(AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> notification, CancellationToken cancellationToken)
    {
        if (notification.Context is not SingleFeedbackAnalysisWorkflowContext context)
            return Task.CompletedTask;

        // TODO : add throwing exception if status is null

        if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
        {
            var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId) ?? throw new InvalidOperationException(
                $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}"
            );

            if (lastHistory.Result is null)
                throw new InvalidOperationException(
                    $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null"
                );

            context.Result.FeedbackRelevance.IsRelevant = JsonConvert.DeserializeObject<bool>(lastHistory.Result!);
        }

        if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        {
        }

        if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.PersonalInformationRedaction)
        {
            context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.ExtractedRelevantContent;
        }

        if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.OpinionMining)
        {
            context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.PiiRedactedContent;
        }

        return Task.CompletedTask;
    }
}