using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.Common.AnalysisWorkflowExecutionOptions.Services;
using Feedback.Analyzer.Application.Common.Workflows.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Services;

/// <summary>
/// Provides feedback analysis workflow orchestration functionality
/// </summary>
public class FeedbackBatchAnalysisWorkflowOrchestrationService(
    IMapper mapper,
    IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService,
    IWorkflowExecutionOptionsService workflowExecutionOptionsService
) : IFeedbackBatchAnalysisWorkflowOrchestrationService
{
    public async ValueTask ExecuteWorkflowAsync(Guid workflowId, CancellationToken cancellationToken = default)
    {
        var queryOptions = new QueryOptions
        {
            AsNoTracking = true
        };

        // TODO : load customer Ids first, then load customer feedbacks in each workflow execution
        // Load analysis workflow
        var analysisWorkflow = await feedbackAnalysisWorkflowService
                                   .Get(workflow => workflow.Id == workflowId, queryOptions)
                                   .Include(workflow => workflow.Product)
                                   .Include(workflow => workflow.Product.Organization)
                                   .AsSplitQuery()
                                   .ProjectTo<FeedbackAnalysisWorkflowContext>(mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync(cancellationToken) ??
                               throw new InvalidOperationException($"Could not execute prompt, workflow with id {workflowId} not found.");

        // Load workflow execution options
        analysisWorkflow.EntryExecutionOption =
            await workflowExecutionOptionsService.GetByIdForExecutionAsync(analysisWorkflow.EntryExecutionOption.Id, queryOptions, cancellationToken) ??
            throw new InvalidOperationException(
                $"Could not execute workflow, workflow execution options with id {analysisWorkflow.EntryExecutionOption.Id} not found."
            );
        
        // Create and publish event for each feedback
    }
}