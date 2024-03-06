using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a repository for managing feedback workflows.
/// </summary>
public class FeedbackWorkflowRepository(AppDbContext appDbContext) 
    : EntityRepositoryBase
        <FeedbackWorkflow, AppDbContext>(
            appDbContext
            ), IFeedbackWorkflowRepository
{
    public new IQueryable<FeedbackWorkflow> Get(Expression<Func<FeedbackWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default)
        => base.Get(predicate, queryOptions);

    public new ValueTask<FeedbackWorkflow?> GetByIdAsync(Guid workflowId, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default)
        => base.GetByIdAsync(workflowId, queryOptions, cancellationToken);

    public new ValueTask<FeedbackWorkflow> CreateAsync(FeedbackWorkflow feedbackWorkflow, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
        => base.CreateAsync(feedbackWorkflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackWorkflow> UpdateAsync(FeedbackWorkflow feedbackWorkflow,
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
        => base.UpdateAsync(feedbackWorkflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackWorkflow> DeleteAsync(FeedbackWorkflow feedbackWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.DeleteAsync(feedbackWorkflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackWorkflow> DeleteByIdAsync(Guid workflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(workflowId, commandOptions, cancellationToken);
}