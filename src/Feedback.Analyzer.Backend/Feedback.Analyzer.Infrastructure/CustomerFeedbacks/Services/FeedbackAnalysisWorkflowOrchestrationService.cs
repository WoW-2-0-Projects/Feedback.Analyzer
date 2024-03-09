using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflowExecutionOptions.Services;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Services;

/// <summary>
/// Provides feedback analysis workflow orchestration functionality
/// </summary>
public class FeedbackAnalysisWorkflowOrchestrationService(
    IWorkflowExecutionService workflowExecutionService,
    IFeedbackAnalysisWorkflowRepository feedbackAnalysisWorkflowRepository,
    WorkflowExecutionOptionsService workflowExecutionOptionsService,
    // IWorkflowExecutionService workflowExecutionService,
    IFeedbackAnalysisOrchestrationService feedbackAnalysisOrchestrationService
) : IIFeedbackAnalysisWorkflowOrchestrationService
{
    public async ValueTask ExecuteWorkflowAsync(Guid workflowId, CancellationToken cancellationToken = default)
    {
        var analysisWorkflow = await feedbackAnalysisWorkflowRepository
                                   .Get(
                                       workflow => workflow.Id == workflowId,
                                       new QueryOptions
                                       {
                                           AsNoTracking = true
                                       }
                                   )
                                   // .Include(workflow => workflow.StartingExecutionOption)
                                   // .ThenInclude(options => options.AnalysisPromptCategory)
                                   .Include(workflow => workflow.Product)
                                   .ThenInclude(workflow => workflow.CustomerFeedbacks)
                                   .AsSplitQuery()
                                   .FirstOrDefaultAsync(cancellationToken) ??
                               throw new InvalidOperationException($"Could not execute prompt, workflow with id {workflowId} not found.");

        analysisWorkflow.StartingExecutionOption =
            await workflowExecutionOptionsService.GetByIdForExecutionAsync(analysisWorkflow.StartingExecutionOptionId, cancellationToken) ??
            throw new InvalidOperationException(
                $"Could not execute workflow, workflow execution options with id {analysisWorkflow.StartingExecutionOptionId} not found."
            );

        //
        // var executionContext = new PromptExecutionContext
        // {
        //     Arguments = new Dictionary<string, string>
        //     {
        //         { $"{PromptConstants.ProductDescription}", feedback.Product.Description },
        //         { $"{PromptConstants.CustomerFeedback}", feedback.Comment }
        //     },
        //     ExecutionHistories = new Dictionary<Guid, PromptExecutionHistory>(),
        // };
        //
        // await workflowExecutionService.ExecuteAsync(workflow.StartingExecutionOption, workflow.Product.CustomerFeedbacks.First(), cancellationToken);

        // workflowExecutionService.ExecuteAsync()

        // workflowExecutionService.ExecuteAsync()
    }
}