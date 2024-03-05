using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

public class FeedbackExecutionWorkflowRepository(AppDbContext dbContext)
    : EntityRepositoryBase<FeedbackExecutionWorkflow, AppDbContext>(dbContext), IFeedbackExecutionWorkflowRepository
{
    public new IQueryable<FeedbackExecutionWorkflow> Get(
        Expression<Func<FeedbackExecutionWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        base.Get(predicate, queryOptions);

    public new ValueTask<FeedbackExecutionWorkflow?> GetByIdAsync(
        Guid workflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.GetByIdAsync(workflowId, queryOptions, cancellationToken);

    public new ValueTask<FeedbackExecutionWorkflow> CreateAsync(
        FeedbackExecutionWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.CreateAsync(workflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackExecutionWorkflow> UpdateAsync(
        FeedbackExecutionWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.UpdateAsync(workflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackExecutionWorkflow?> DeleteAsync(
        FeedbackExecutionWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.DeleteAsync(workflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackExecutionWorkflow?> DeleteByIdAsync(
        Guid workflowId,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default
    ) =>
        base.DeleteByIdAsync(workflowId, commandOptions, cancellationToken);
}