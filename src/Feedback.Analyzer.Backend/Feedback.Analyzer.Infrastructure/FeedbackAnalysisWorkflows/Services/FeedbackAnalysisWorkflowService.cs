using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

public class FeedbackAnalysisWorkflowService(IFeedbackAnalysisWorkflowRepository feedbackAnalysisWorkflowRepository)
    : IFeedbackAnalysisWorkflowService
{
    public IQueryable<FeedbackAnalysisWorkflow> Get(
        Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        feedbackAnalysisWorkflowRepository.Get(predicate, queryOptions);

    public ValueTask<FeedbackAnalysisWorkflow?> GetByIdAsync(
        Guid workflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.GetByIdAsync(workflowId, queryOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflow> CreateAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.CreateAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflow> UpdateAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.UpdateAsync(workflow, commandOptions, cancellationToken);

    public async ValueTask<bool> UpdateStatus(Guid workflowId, WorkflowStatus status, CancellationToken cancellationToken = default)
    {
        var updatedWorkflowsCount =  await feedbackAnalysisWorkflowRepository.UpdateBatchAsync(
            setPropertyCalls => setPropertyCalls.SetProperty(workflow => workflow.Status, status),
            workflow => workflow.Id == workflowId,
            cancellationToken
        );

        return updatedWorkflowsCount == 1;
    }

    public ValueTask<FeedbackAnalysisWorkflow?> DeleteAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.DeleteAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflow?> DeleteByIdAsync(
        Guid workflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.DeleteByIdAsync(workflowId, commandOptions, cancellationToken);
}