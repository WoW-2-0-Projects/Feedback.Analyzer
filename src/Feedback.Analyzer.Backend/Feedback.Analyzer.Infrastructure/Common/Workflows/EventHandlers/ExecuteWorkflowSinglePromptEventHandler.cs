using Feedback.Analyzer.Application.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

/// <summary>
/// Represents a class that handles the execution of a workflow prompt in response to a single prompt event.
/// </summary>
public class ExecuteWorkflowSinglePromptEventHandler(
    IFeedbackAnalysisWorkflowRepository workflowService,
    IPromptExecutionProcessingService promptExecutionProcessingService
) : IEventHandler<ExecuteWorkflowSinglePromptEvent>
{
    public async Task Handle(ExecuteWorkflowSinglePromptEvent notification, CancellationToken cancellationToken)
    {
        var workflow = await workflowService.Get(
                                                workflow => workflow.Id == notification.WorkflowId,
                                                new QueryOptions
                                                {
                                                    AsNoTracking = true
                                                }
                                            )
                                            .Include(workflow => workflow.AnalysisWorkflow)
                                            .Include(workflow => workflow.Product)
                                            .ThenInclude(product => product.CustomerFeedbacks)
                                            .AsSplitQuery()
                                            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                       throw new InvalidOperationException(
                           $"Could not execute prompt, workflow with id {notification.WorkflowId} not found.");

        var arguments = new Dictionary<string, string>
        {
            { "productDescription", workflow.Product.Description },
            { "customerFeedback", workflow.Product.CustomerFeedbacks.First().Comment }
        };

        var histories = await promptExecutionProcessingService.ExecuteAsync(notification.PromptId, arguments,
            cancellationToken: cancellationToken);
        
    }
}