using Feedback.Analyzer.Application.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

/// <summary>
/// Represents a class that handles the execution of a workflow prompt in response to a single prompt event.
/// </summary>
public class ExecuteWorkflowSinglePromptEventHandler(IServiceScopeFactory serviceScopeFactory) : IEventHandler<ExecuteWorkflowSinglePromptEvent>
{
    public async Task Handle(ExecuteWorkflowSinglePromptEvent notification, CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

        var promptService = scopedServiceProvider.GetRequiredService<IPromptService>();
        var workflowService = scopedServiceProvider.GetRequiredService<IFeedbackAnalysisWorkflowRepository>();
        var promptExecutionProcessingService = scopedServiceProvider.GetRequiredService<IPromptExecutionProcessingService>();
        
        
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
            { PromptConstants.ProductDescription, workflow.Product.Description },
            { PromptConstants.CustomerFeedback, workflow.Product.CustomerFeedbacks.First().Comment }
        };

        // Query prompt and feedback
        var prompt = await promptService.GetByIdAsync(notification.PromptId, cancellationToken: cancellationToken) ??
                     throw new InvalidOperationException(
                         $"Could not execute prompt, prompt with id {notification.PromptId} not found.");

        var histories = await promptExecutionProcessingService.ExecuteAsync(prompt, arguments,
            cancellationToken: cancellationToken);
    }
}