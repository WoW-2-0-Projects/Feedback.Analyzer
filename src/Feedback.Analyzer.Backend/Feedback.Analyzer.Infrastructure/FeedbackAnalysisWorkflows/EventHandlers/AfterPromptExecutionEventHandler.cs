using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;
using MassTransit;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
///  Represents event handler for before prompt execution hook event
/// </summary>
public class AfterPromptExecutionEventHandler : EventHandlerBase<AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
{
    protected override ValueTask HandleAsync(AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> @event, CancellationToken cancellationToken)
    {
        if (@event.Context is not SingleFeedbackAnalysisWorkflowContext context)
            return ValueTask.CompletedTask;

        // TODO : add throwing exception if status is null

        if (@event.Prompt.Category.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
        {
            var lastHistory = context.GetLastHistory(@event.Prompt.CategoryId) ?? throw new InvalidOperationException(
                $"No history found for the category - {@event.Prompt.Category.Category.GetDisplayName()}"
            );

            if (lastHistory.Result is null)
                throw new InvalidOperationException(
                    $"Result of the last history is null for the category - {@event.Prompt.Category.Category.GetDisplayName()} is null"
                );

            context.Result.FeedbackRelevance.IsRelevant = JsonConvert.DeserializeObject<bool>(lastHistory.Result!);

            if (!context.Result.FeedbackRelevance.IsRelevant)
                throw new InvalidOperationException("Feedback is not relevant");
        }

        if (@event.Prompt.Category.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        {
            var lastHistory = context.GetLastHistory(@event.Prompt.CategoryId) ?? throw new InvalidOperationException(
                $"No history found for the category - {@event.Prompt.Category.Category.GetDisplayName()}"
            );

            if (lastHistory.Result is null)
                throw new InvalidOperationException(
                    $"Result of the last history is null for the category - {@event.Prompt.Category.Category.GetDisplayName()} is null"
                );

            context.Result.FeedbackRelevance.ExtractedRelevantContent = lastHistory.Result;
        }

        if (@event.Prompt.Category.Category == FeedbackAnalysisPromptCategory.PersonalInformationRedaction)
        {
            var lastHistory = context.GetLastHistory(@event.Prompt.CategoryId) ?? throw new InvalidOperationException(
                $"No history found for the category - {@event.Prompt.Category.Category.GetDisplayName()}"
            );

            if (lastHistory.Result is null)
                throw new InvalidOperationException(
                    $"Result of the last history is null for the category - {@event.Prompt.Category.Category.GetDisplayName()} is null"
                );

            context.Result.FeedbackRelevance.PiiRedactedContent = lastHistory.Result;
        }

        if (@event.Prompt.Category.Category == FeedbackAnalysisPromptCategory.OpinionMining)
        {
            context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.PiiRedactedContent;
        }

        return ValueTask.CompletedTask;
    }
}