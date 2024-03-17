using Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Provides feedback analysis processing service functionality
/// </summary>
public class FeedbackAnalysisWorkflowProcessingService(
    IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService,
    IAnalysisWorkflowService analysisWorkflowService,
    IRequestContextProvider requestContextProvider,
    IProductService productService
) : IFeedbackAnalysisWorkflowProcessingService
{
    public async ValueTask<(FeedbackAnalysisWorkflow FeedbackAnalysisWorkflow, AnalysisWorkflow AnalysisWorkflow)> CreateAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        AnalysisWorkflow analysisWorkflow,
        Guid baseWorkflowId,
        CancellationToken cancellationToken = default
    )
    {
        // Query given product client Id
        var currentProductClientId = await productService
            .Get(product => product.Id == feedbackAnalysisWorkflow.ProductId)
            .Include(product => product.Organization)
            .Select(product => product.Organization.ClientId)
            .FirstOrDefaultAsync(cancellationToken);

        // Validate before creation
        if (currentProductClientId == Guid.Empty || currentProductClientId != requestContextProvider.GetUserId())
            throw new UnauthorizedAccessException("Client id must match the current user id");

        // Query base workflow
        var baseWorkflow = await feedbackAnalysisWorkflowService
            .Get(workflow => workflow.Id == baseWorkflowId)
            .Include(workflow => workflow.AnalysisWorkflow)
            .ThenInclude(workflow => workflow.EntryExecutionOption)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException($"Base workflow with id {baseWorkflowId} not found");

        // Clone execution options from base workflow
        var clonedEntryExecutionOption = baseWorkflow.AnalysisWorkflow.EntryExecutionOption.Clone();

        // Create new analysis workflow
        analysisWorkflow.EntryExecutionOption = clonedEntryExecutionOption;
        var createdAnalysisWorkflow = await analysisWorkflowService.CreateAsync(analysisWorkflow, cancellationToken: cancellationToken);

        feedbackAnalysisWorkflow.AnalysisWorkflow = createdAnalysisWorkflow;
        feedbackAnalysisWorkflow.Id = createdAnalysisWorkflow.Id;

        return (await feedbackAnalysisWorkflowService.CreateAsync(feedbackAnalysisWorkflow, cancellationToken: cancellationToken), analysisWorkflow);
    }

    public async ValueTask<(FeedbackAnalysisWorkflow FeedbackAnalysisWorkflow, AnalysisWorkflow AnalysisWorkflow)> UpdateAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        AnalysisWorkflow analysisWorkflow,
        CancellationToken cancellationToken = default
    )
    {
        var updatedAnalysisWorkflow = await analysisWorkflowService.UpdateAsync(analysisWorkflow, cancellationToken: cancellationToken);

        return (feedbackAnalysisWorkflow, updatedAnalysisWorkflow);
    }
}