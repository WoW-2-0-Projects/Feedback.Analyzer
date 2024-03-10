using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

public class FeedbackAnalysisOrchestrationService(
    IEventBusBroker eventBusBroker,
    ICustomerFeedbackService customerFeedbackService,
    IPromptExecutionProcessingService promptExecutionProcessingService,
    IFeedbackAnalysisResultService feedbackAnalysisResultService
) : IFeedbackAnalysisOrchestrationService
{
    public async ValueTask ExecuteWorkflowAsync(SingleFeedbackAnalysisWorkflowContext context, CancellationToken cancellationToken = default)
    {
        var queryOptions = new QueryOptions
        {
            TrackingMode = QueryTrackingMode.AsNoTracking
        };

        var feedback = await customerFeedbackService.GetByIdAsync(context.FeedbackId, queryOptions, cancellationToken) ??
                       throw new InvalidOperationException($"Could not execute prompt, feedback with id {context.FeedbackId} not found.");

        context.Status = WorkflowStatus.Running;
        context.Arguments[PromptConstants.ProductDescription] = context.Product.Description;
        context.Arguments[PromptConstants.CustomerFeedback] = feedback.Comment;

        // Execute whole workflow
        await ExecuteOptionAsync(context, context.EntryExecutionOption, cancellationToken);

        // Create feedback analysis result
        await feedbackAnalysisResultService.CreateAsync(context.Result, cancellationToken: cancellationToken);
    }

    private async ValueTask ExecuteOptionAsync(WorkflowContext context, WorkflowExecutionOption option, CancellationToken cancellationToken = default)
    {
        if (context.Status != WorkflowStatus.Running)
            return;

        // Execute option
        var executePromptAction = () => ExecutePromptAsync(context, option.AnalysisPromptCategory.SelectedPrompt!, cancellationToken);
        var promptResult = await executePromptAction.GetValueAsync();
        if (!promptResult.IsSuccess)
        {
            context.Status = WorkflowStatus.Failed;
            context.ErrorMessage =
                $"Failed to execute prompt - {option.AnalysisPromptCategory.Category.GetDisplayName()}, Version - {option.AnalysisPromptCategory.SelectedPrompt!.Version}.{option.AnalysisPromptCategory.SelectedPrompt.Revision}, Reason - {promptResult.Exception!.Message}";
        }

        // Execute child options
        if (option.ChildExecutionOptions is not null && option.ChildExecutionOptions.Count > 0)
            await Task.WhenAll(
                option.ChildExecutionOptions.Select(childOption => ExecuteOptionAsync(context, childOption, cancellationToken).AsTask())
            );
    }

    private async ValueTask ExecutePromptAsync(WorkflowContext context, AnalysisPrompt prompt, CancellationToken cancellationToken = default)
    {
        // Publish before prompt execution event
        await eventBusBroker.PublishLocalAsync(
            new BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>
            {
                Context = context,
                Prompt = prompt
            }
        );

        // Execute prompt and add histories to context
        var histories = await promptExecutionProcessingService.ExecuteAsync(prompt, context.Arguments, 1, cancellationToken);
        foreach (var groupedHistory in histories.GroupBy(history => history.Prompt.CategoryId))
            context.Histories.Add(groupedHistory);

        // Publish after prompt execution event
        await eventBusBroker.PublishLocalAsync(
            new AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>
            {
                Context = context,
                Prompt = prompt
            }
        );
    }
}