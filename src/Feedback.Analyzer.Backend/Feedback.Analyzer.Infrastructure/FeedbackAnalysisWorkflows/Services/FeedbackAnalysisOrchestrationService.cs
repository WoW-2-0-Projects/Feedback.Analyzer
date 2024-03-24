using System.Diagnostics;
using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// FeedbackAnalysisOrchestrationService orchestrates the workflow for analyzing customer feedback. It utilizes various services
/// including event bus, customer feedback service, feedback analysis result service, and prompt execution processing service to
/// execute the analysis workflow. The workflow involves executing prompts associated with the feedback and generating analysis results.
/// It supports the execution of a single feedback analysis workflow.
/// </summary>
public class FeedbackAnalysisOrchestrationService(
    IEventBusBroker eventBusBroker,
    ICustomerFeedbackService customerFeedbackService,
    IFeedbackAnalysisResultService feedbackAnalysisResultService,
    IPromptExecutionProcessingService promptExecutionProcessingService
) : IFeedbackAnalysisOrchestrationService
{
    public async ValueTask ExecuteWorkflowAsync(SingleFeedbackAnalysisWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        var queryOptions = new QueryOptions
        {
            TrackingMode = QueryTrackingMode.AsNoTracking
        };

        var feedback = await customerFeedbackService.GetByIdAsync(context.FeedbackId, queryOptions, cancellationToken)
                       ?? throw new InvalidOperationException(
                           $"Could not execute prompt, feedback with id {context.FeedbackId} not found");

        context.Status = WorkflowStatus.Running;
        context.Arguments[PromptConstants.ProductDescription] = context.Product.Description;
        context.Arguments[PromptConstants.CustomerFeedback] = feedback.Comment;
        
        // Execute whole workflow for single feedback
        var stopwatch = Stopwatch.StartNew();
        await ExecuteOptionAsync(context, context.EntryExecutionOption, cancellationToken);
        stopwatch.Stop();
        
        if (context.Status == WorkflowStatus.Running)
            context.Status = WorkflowStatus.Completed;

        context.Result.AnalysisResult.AnalysisDuration = stopwatch.Elapsed;
        context.Result.AnalysisResult.Status = context.Status;
        context.Result.AnalysisResult.ErrorMessage = context.ErrorMessage;
        context.Result.AnalysisResult.ModelExecutionDuration = TimeSpan.FromMilliseconds(context.Histories
            .Sum(groupedHistory => groupedHistory
                .Sum(history => history.ExecutionDuration.TotalMilliseconds)));
        
        // Create feedback analysis result
        await feedbackAnalysisResultService.CreateAsync(context.Result, cancellationToken: cancellationToken);
    }

    private async ValueTask ExecuteOptionAsync(WorkflowContext context, WorkflowExecutionOption option, CancellationToken cancellationToken = default)
    {
        if (context.Status != WorkflowStatus.Running)
            return;
        
        // Fix selected prompt category reference
        option.AnalysisPromptCategory.SelectedPrompt!.Category = option.AnalysisPromptCategory;

        // Execute option
        var executePromptAction = () => ExecutePromptAsync(context, option.AnalysisPromptCategory.SelectedPrompt!, 
            new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token);
        var promptResult = await executePromptAction.GetValueAsync();
        if (!promptResult.IsSuccess)
        {
            context.Status = WorkflowStatus.Failed;
            context.ErrorMessage = promptResult.Exception!.Message;
        }
        
        // Execute child options
        if (option.ChildExecutionOptions is not null && option.ChildExecutionOptions.Count > 0)
            await Task.WhenAll(
                option.ChildExecutionOptions.Select(childOption => ExecuteOptionAsync(context, childOption, cancellationToken).AsTask())
            );
    }

    private async ValueTask ExecutePromptAsync(WorkflowContext context, AnalysisPrompt prompt,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return;
        
        // Publish before prompt execution event
        await eventBusBroker.PublishLocalAsync(
            new BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>
            {
                Context = context,
                Prompt = prompt
            });
        
        // Execute prompt and add histories to context
        var histories = await promptExecutionProcessingService.ExecuteAsync(prompt, context.Arguments, 1, cancellationToken);
        foreach (var groupedHistory in histories.GroupBy(history => history.PromptCategoryId))
            context.Histories.Add(groupedHistory);
        
        // Publish after prompt execution event
        await eventBusBroker.PublishLocalAsync(
            new AfterPromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>
            {
                Context = context,
                Prompt = prompt
            });
    }
}