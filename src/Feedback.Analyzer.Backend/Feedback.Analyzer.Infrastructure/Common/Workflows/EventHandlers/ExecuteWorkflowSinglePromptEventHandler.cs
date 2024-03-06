using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.Common.Workflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

public class ExecuteWorkflowSinglePromptEventHandler(
    IFeedbackExecutionWorkflowService workflowService,
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
                           .Include(workflow => workflow.Product)
                           .ThenInclude(product => product.CustomerFeedbacks)
                           .AsSplitQuery()
                           .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                       throw new InvalidOperationException($"Could not execute prompt, workflow with id {notification.WorkflowId} not found.");

        var arguments = new Dictionary<string, string>
        {
            { "productDescription", workflow.Product.Description },
            { "customerFeedback", workflow.Product.CustomerFeedbacks.First().Comment }
        };

        var histories = await promptExecutionProcessingService.ExecuteAsync(notification.PromptId, arguments, cancellationToken: cancellationToken);

        // Query prompt category
        // var promptCategory = 
        // var history = histories.First();
        
        // Send command to map history result based on prompt category

        var result = "";
    }
}