using System.Collections.Immutable;
using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing client entities.
/// </summary>
public class PromptCategoryRepository(AppDbContext dbContext)
    : EntityRepositoryBase<AnalysisPromptCategory, AppDbContext>(dbContext), IPromptCategoryRepository
{
    public new IQueryable<AnalysisPromptCategory> Get(
        Expression<Func<AnalysisPromptCategory, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        base.Get(predicate, queryOptions);

    public new ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<AnalysisPromptCategory>, SetPropertyCalls<AnalysisPromptCategory>>> setPropertyCalls,
        Expression<Func<AnalysisPromptCategory, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    ) =>
        base.UpdateBatchAsync(setPropertyCalls, batchUpdatePredicate, cancellationToken);
}