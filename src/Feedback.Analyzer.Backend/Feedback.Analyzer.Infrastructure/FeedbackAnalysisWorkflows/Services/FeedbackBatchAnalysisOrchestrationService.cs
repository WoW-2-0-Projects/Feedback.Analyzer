using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.Common.AnalysisWorkflowExecutionOptions.Services;
using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Provides feedback analysis workflow orchestration functionality
/// </summary>
public class FeedbackBatchAnalysisOrchestrationService(
    IMapper mapper,
    IEventBusBroker eventBusBroker,
    IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService,
    IWorkflowExecutionOptionsService workflowExecutionOptionsService,
    IFeedbackAnalysisWorkflowResultService feedbackAnalysisWorkflowResultService
) : IFeedbackBatchAnalysisOrchestrationService
{
    public async ValueTask RunWorkflowAsync(Guid workflowId, CancellationToken cancellationToken = default)
    {
        var queryOptions = new QueryOptions
        {
            AsNoTracking = true
        };

        // TODO : load customer Ids first, then load customer feedbacks in each workflow execution

        // Load analysis workflow
        var workflowContext = await feedbackAnalysisWorkflowService
                                  .Get(workflow => workflow.Id == workflowId, queryOptions)
                                  .Include(workflow => workflow.Product)
                                  .Include(workflow => workflow.Product.Organization)
                                  .Include(workflow => workflow.Product.CustomerFeedbacks)
                                  .AsSplitQuery()
                                  .ProjectTo<FeedbackAnalysisWorkflowContext>(mapper.ConfigurationProvider)
                                  .FirstOrDefaultAsync(cancellationToken) ??
                              throw new InvalidOperationException($"Could not execute prompt, workflow with id {workflowId} not found.");

        // Load workflow execution options
        workflowContext.EntryExecutionOption =
            await workflowExecutionOptionsService.GetByIdForExecutionAsync(
                workflowContext.EntryExecutionOption.Id,
                queryOptions,
                cancellationToken
            ) ?? throw new InvalidOperationException(
                $"Could not execute workflow, workflow execution options with id {workflowContext.EntryExecutionOption.Id} not found."
            );

        // Validate workflow

        //  Update workflow status
        var updateResult = await feedbackAnalysisWorkflowService.UpdateStatus(workflowId, WorkflowStatus.Running, cancellationToken);
        if (!updateResult)
            throw new InvalidOperationException($"Could not execute workflow, workflow with id {workflowId} not found.");

        // Create workflow result and add to context
        var workflowResult = new FeedbackAnalysisWorkflowResult
        {
            WorkflowId = workflowContext.WorkflowId,
            FeedbacksCount = (ulong)workflowContext.FeedbacksId.Count
        };

        var createdWorkflowResult = await feedbackAnalysisWorkflowResultService.CreateAsync(workflowResult, cancellationToken: cancellationToken);
        workflowContext.WorkflowResultId = createdWorkflowResult.Id;

        // Create and publish event for each feedback
        var analyzeFeedbackEventPublishTasks = workflowContext.FeedbacksId
            .Select(
                feedbackId =>
                {
                    var singleFeedbackAnalysisWorkflowContext = mapper.Map<SingleFeedbackAnalysisWorkflowContext>(workflowContext);
                    singleFeedbackAnalysisWorkflowContext.FeedbackId = feedbackId;

                    return singleFeedbackAnalysisWorkflowContext;
                }
            )
            .Select(
                feedbackAnalysisContext => eventBusBroker.PublishAsync(
                    new AnalyzeFeedbackEvent
                    {
                        Context = feedbackAnalysisContext
                    }
                ).AsTask()
            );

        await Task.WhenAll(analyzeFeedbackEventPublishTasks);
    }
}