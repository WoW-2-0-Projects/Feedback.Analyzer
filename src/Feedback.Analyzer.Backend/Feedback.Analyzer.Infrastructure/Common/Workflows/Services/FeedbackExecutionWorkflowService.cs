using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.Workflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.Services;

public class FeedbackExecutionWorkflowService(IFeedbackExecutionWorkflowRepository feedbackExecutionWorkflowRepository)
    : IFeedbackExecutionWorkflowService
{
    public IQueryable<FeedbackExecutionWorkflow> Get(
        Expression<Func<FeedbackExecutionWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        feedbackExecutionWorkflowRepository.Get(predicate, queryOptions);

    public ValueTask<FeedbackExecutionWorkflow?> GetByIdAsync(
        Guid workflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackExecutionWorkflowRepository.GetByIdAsync(workflowId, queryOptions, cancellationToken);

    public ValueTask<FeedbackExecutionWorkflow> CreateAsync(
        FeedbackExecutionWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackExecutionWorkflowRepository.CreateAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackExecutionWorkflow> UpdateAsync(
        FeedbackExecutionWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackExecutionWorkflowRepository.UpdateAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackExecutionWorkflow?> DeleteAsync(
        FeedbackExecutionWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackExecutionWorkflowRepository.DeleteAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackExecutionWorkflow?> DeleteByIdAsync(
        Guid workflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackExecutionWorkflowRepository.DeleteByIdAsync(workflowId, commandOptions, cancellationToken);
}