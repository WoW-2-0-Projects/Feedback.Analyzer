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
public class AfterPromptExecutionEventHandler : IEventHandler<AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
{
    public Task Handle(AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> notification, CancellationToken cancellationToken)
    {
        if (notification.Context is not SingleFeedbackAnalysisWorkflowContext context)
            return Task.CompletedTask;
        
        var lastHistory = context.GetLastHistory(notification.Prompt.CategoryId);

        if (lastHistory is null)
        {
            context.Status = WorkflowStatus.Failed;
            context.ErrorMessage = $"No history found for the category - {notification.Prompt.Category.Category.GetDisplayName()}";

            return Task.CompletedTask;
        }

        if (lastHistory.Result is null)
        {
            context.Status = WorkflowStatus.Failed;
            context.ErrorMessage =
                $"Result of the last history is null for the category - {notification.Prompt.Category.Category.GetDisplayName()} is null";

            return Task.CompletedTask;
        }
        
        switch (notification.Prompt.Category.Category)
        {
            case FeedbackAnalysisPromptCategory.RelevanceAnalysis:
            {
                context.Result.FeedbackRelevance.IsRelevant = JsonConvert.DeserializeObject<bool>(lastHistory.Result!.ToLower());

                if (!context.Result.FeedbackRelevance.IsRelevant)
                {
                    context.Status = WorkflowStatus.Completed;
                    context.ErrorMessage = "Feedback is not relevant";
                }
                break;
            }
            case FeedbackAnalysisPromptCategory.RelevantContentExtraction:
            {
                context.Result.FeedbackRelevance.ExtractedRelevantContent = lastHistory.Result;
                break;
            }
            case FeedbackAnalysisPromptCategory.PersonalInformationRedaction:
            {
                context.Result.FeedbackRelevance.PiiRedactedContent = lastHistory.Result;
                break;
            }
            case FeedbackAnalysisPromptCategory.OpinionMining:
                context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.PiiRedactedContent;
                break;
        }

        return Task.CompletedTask;
    }
}