using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

public class ExecuteWorkflowSinglePromptEventHandler(IServiceScopeFactory serviceScopeFactory) : EventHandlerBase<ExecuteWorkflowSinglePromptEvent>
{
    protected override async ValueTask HandleAsync(ExecuteWorkflowSinglePromptEvent @event, CancellationToken cancellationToken)
    {
       var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

        var promptService = scopedServiceProvider.GetRequiredService<IPromptService>();
        var workflowService = scopedServiceProvider.GetRequiredService<IFeedbackAnalysisWorkflowService>();
        var promptExecutionProcessingService = scopedServiceProvider.GetRequiredService<IPromptExecutionProcessingService>();

        var workflow = await workflowService.Get(
                               workflow => workflow.Id == @event.WorkflowId,
                               new QueryOptions
                               {
                                   TrackingMode = QueryTrackingMode.AsNoTracking
                               }
                           )
                           .Include(workflow => workflow.Product)
                           .ThenInclude(product => product.CustomerFeedbacks)
                           .AsSplitQuery()
                           .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                       throw new InvalidOperationException($"Could not execute prompt, workflow with id {@event.WorkflowId} not found.");

        var arguments = new Dictionary<string, string>
        {
            { PromptConstants.ProductDescription, workflow.Product.Description },
            { PromptConstants.CustomerFeedback, workflow.Product.CustomerFeedbacks.First().Comment }
        };

        var prompt = await promptService.GetByIdAsync(
            @event.PromptId,
            queryOptions: new QueryOptions
            {
                TrackingMode = QueryTrackingMode.AsTracking
            },
            cancellationToken: cancellationToken
        ) ?? throw new InvalidOperationException($"Could not execute prompt, prompt with id {@event.PromptId} not found.");

        await promptExecutionProcessingService.ExecuteAsync(prompt, arguments, cancellationToken: cancellationToken);
    }
}