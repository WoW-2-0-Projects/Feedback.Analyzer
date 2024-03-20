using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
/// Represents event handler for before prompt execution hook event
/// </summary>
public class AfterPromptExecutionEventHandler : EventHandlerBase<AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
{
    protected override ValueTask HandleAsync(
        AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> notification,
        CancellationToken cancellationToken
    )
    {
        if (notification.Context is not SingleFeedbackAnalysisWorkflowContext context)
            return ValueTask.CompletedTask;

        switch (notification.Prompt.Category.Category)
        {
            case FeedbackAnalysisPromptCategory.RelevanceAnalysis:
            {
                var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId) ?? throw new InvalidOperationException(
                    $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}"
                );

                if (lastHistory.Result is null)
                    throw new InvalidOperationException(
                        $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null"
                    );

                context.Result.FeedbackRelevance.IsRelevant = JsonConvert.DeserializeObject<bool>(lastHistory.Result!);

                if (!context.Result.FeedbackRelevance.IsRelevant)
                    throw new InvalidOperationException("Feedback is not relevant");
                break;
            }
            case FeedbackAnalysisPromptCategory.LanguageRecognition:
            {
                var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId) ?? throw new InvalidOperationException(
                    $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}"
                );

                if (lastHistory.Result is null)
                    throw new InvalidOperationException(
                        $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null"
                    );

                context.Result.FeedbackRelevance.RecognizedLanguages = JsonConvert.DeserializeObject<string[]>(lastHistory.Result!)
                    ?? throw new InvalidOperationException("Failed to deserialize the recognized languages");

                break;
            }
            case FeedbackAnalysisPromptCategory.RelevantContentExtraction:
            {
                var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId) ?? throw new InvalidOperationException(
                    $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}"
                );

                if (lastHistory.Result is null)
                    throw new InvalidOperationException(
                        $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null"
                    );

                context.Result.FeedbackRelevance.ExtractedRelevantContent = lastHistory.Result;
                break;
            }
            case FeedbackAnalysisPromptCategory.PersonalInformationRedaction:
            {
                var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId) ?? throw new InvalidOperationException(
                    $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}"
                );

                if (lastHistory.Result is null)
                    throw new InvalidOperationException(
                        $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null"
                    );

                context.Result.FeedbackRelevance.PiiRedactedContent = lastHistory.Result;
                break;
            }
            case FeedbackAnalysisPromptCategory.OpinionMining:
            {
                var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId) ?? throw new InvalidOperationException(
                    $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}"
                );

                if (lastHistory.Result is null)
                    throw new InvalidOperationException(
                        $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null"
                    );

                context.Result.FeedbackOpinion.OverallOpinion = JsonConvert.DeserializeObject<OpinionType>(lastHistory.Result);
                break;
            }
            case FeedbackAnalysisPromptCategory.OpinionPointsExtraction:
            {
                var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId) ?? throw new InvalidOperationException(
                    $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}"
                );

                if (lastHistory.Result is null)
                    throw new InvalidOperationException(
                        $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null"
                    );

                var points = JsonConvert.DeserializeObject<string[][]>(lastHistory.Result) ??
                             throw new InvalidOperationException("Failed to deserialize the opinion points");

                context.Result.FeedbackOpinion.PositiveOpinionPoints = points[0];
                context.Result.FeedbackOpinion.NegativeOpinionPoints = points[1];
                break;
            }
        }

        return ValueTask.CompletedTask;
    }
}