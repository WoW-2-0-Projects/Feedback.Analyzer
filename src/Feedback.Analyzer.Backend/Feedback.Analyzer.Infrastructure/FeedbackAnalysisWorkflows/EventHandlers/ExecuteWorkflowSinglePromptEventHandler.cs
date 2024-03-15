using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Constants;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

public class ExecuteWorkflowSinglePromptEventHandler(IServiceScopeFactory serviceScopeFactory) : 
    IEventHandler<ExecuteWorkflowSinglePromptEventBase>,
    IConsumer<ExecuteWorkflowSinglePromptEventBase>
{
    public async Task Handle(ExecuteWorkflowSinglePromptEventBase notification, CancellationToken cancellationToken) =>
        await HandleAsync(notification, cancellationToken);

    public async Task Consume(ConsumeContext<ExecuteWorkflowSinglePromptEventBase> context) =>
        await HandleAsync(context.Message, context.CancellationToken);

    private async ValueTask HandleAsync(ExecuteWorkflowSinglePromptEventBase notification, CancellationToken cancellationToken)
    {
        var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

        var promptService = scopedServiceProvider.GetRequiredService<IPromptService>();
        var workflowService = scopedServiceProvider.GetRequiredService<IFeedbackAnalysisWorkflowService>();
        var promptExecutionProcessingService = scopedServiceProvider.GetRequiredService<IPromptExecutionProcessingService>();

        var workflow = await workflowService.Get(
                               workflow => workflow.Id == notification.WorkflowId,
                               new QueryOptions
                               {
                                   TrackingMode = QueryTrackingMode.AsNoTracking
                               }
                           )
                           .Include(workflow => workflow.Product)
                           .ThenInclude(product => product.CustomerFeedbacks)
                           .AsSplitQuery()
                           .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                       throw new InvalidOperationException($"Could not execute prompt, workflow with id {notification.WorkflowId} not found.");

        var arguments = new Dictionary<string, string>
        {
            { PromptConstants.ProductDescription, workflow.Product.Description },
            { PromptConstants.CustomerFeedback, workflow.Product.CustomerFeedbacks.First().Comment }
        };

        var prompt = await promptService.GetByIdAsync(
            notification.PromptId,
            queryOptions: new QueryOptions
            {
                TrackingMode = QueryTrackingMode.AsTracking
            },
            cancellationToken: cancellationToken
        ) ?? throw new InvalidOperationException($"Could not execute prompt, prompt with id {notification.PromptId} not found.");

        await promptExecutionProcessingService.ExecuteAsync(prompt, arguments, cancellationToken: cancellationToken);
    }
}