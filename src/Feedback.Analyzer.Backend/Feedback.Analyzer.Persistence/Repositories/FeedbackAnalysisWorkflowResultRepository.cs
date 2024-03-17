using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository for managing feedback analysis workflow results.
/// </summary>
public class FeedbackAnalysisWorkflowResultRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
    :  EntityRepositoryBase<FeedbackAnalysisWorkflowResult, AppDbContext>(dbContext, cacheBroker), IFeedbackAnalysisWorkflowResultRepository
{
    public new IQueryable<FeedbackAnalysisWorkflowResult> Get(
        Expression<Func<FeedbackAnalysisWorkflowResult, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        base.Get(predicate, queryOptions);

    public new ValueTask<FeedbackAnalysisWorkflowResult?> GetByIdAsync(
        Guid resultId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.GetByIdAsync(resultId, queryOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflowResult> CreateAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.CreateAsync(workflowResult, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflowResult> UpdateAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.UpdateAsync(workflowResult, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflowResult?> DeleteAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.DeleteAsync(workflowResult, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflowResult?> DeleteByIdAsync(
        Guid resultId,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default
    ) =>
        base.DeleteByIdAsync(resultId, commandOptions, cancellationToken);
}