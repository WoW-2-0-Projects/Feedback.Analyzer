using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a repository interface for managing feedback analysis workflows.
/// </summary>
public class FeedbackAnalysisWorkflowRepository(AppDbContext appDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<FeedbackAnalysisWorkflow, AppDbContext>(appDbContext, cacheBroker), IFeedbackAnalysisWorkflowRepository
{
    public new IQueryable<FeedbackAnalysisWorkflow> Get(
        Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default)
        => base.Get(predicate, queryOptions);

    public new ValueTask<FeedbackAnalysisWorkflow?> GetByIdAsync(
        Guid workflowId, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default)
        => base.GetByIdAsync(workflowId, queryOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow> CreateAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.CreateAsync(feedbackAnalysisWorkflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow> UpdateAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.UpdateAsync(feedbackAnalysisWorkflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow?> DeleteAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.DeleteAsync(feedbackAnalysisWorkflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow?> DeleteByIdAsync(
        Guid workflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(workflowId, commandOptions, cancellationToken);
}