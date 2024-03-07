using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.Common.Workflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

public class ExecuteWorkflowEventHandler(IFeedbackExecutionWorkflowService feedbackExecutionWorkflowService,
    IPromptExecutionOrchestrationService promptExecutionOrchestrationService) : IEventHandler<ExecuteWorkflowEvent>
{
    public async Task Handle(ExecuteWorkflowEvent notification, CancellationToken cancellationToken)
    {
        // Query the workflow by its id, including its product and customer feedbacks
        var workflow = await feedbackExecutionWorkflowService
            .Get(workflow => workflow.Id == notification.WorkflowId,
                new QueryOptions
                {
                    AsNoTracking = true
                })
            .Include(workflow => workflow.StartingExecutionOption)
                .ThenInclude(options => options.AnalysisPromptCategory)
            .Include(workflow => workflow.Product)
                .ThenInclude(workflow => workflow.CustomerFeedbacks)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new InvalidOperationException($"Could not execute prompt, workflow with id {notification.WorkflowId} not found.");

         await promptExecutionOrchestrationService
            .ExecuteAsync(workflow.StartingExecutionOption, workflow.Product.CustomerFeedbacks.First(), cancellationToken);
    }
}