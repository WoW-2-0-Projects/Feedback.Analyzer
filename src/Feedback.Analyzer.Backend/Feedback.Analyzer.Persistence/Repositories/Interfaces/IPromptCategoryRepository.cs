using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for managing prompt category entities in the repository.
/// </summary>
public interface IPromptCategoryRepository
{
    /// <summary>
    /// Retrieves a queryable collection of prompt category entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the prompt categories (optional).</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <returns>A queryable collection of prompt category entities.</returns>
    IQueryable<AnalysisPromptCategory> Get(Expression<Func<AnalysisPromptCategory, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Asynchronously updates a batch of analysis prompt categories based on the provided set property calls expression.
    /// </summary>
    /// <param name="setPropertyCalls">An expression specifying the set property calls to be applied to each analysis prompt category.</param>
    /// <param name="batchUpdatePredicate">An optional predicate to filter the analysis prompt categories eligible for batch update. If not specified, all categories will be updated.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation. The result indicates the number of analysis prompt categories updated.</returns>

    ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<AnalysisPromptCategory>, SetPropertyCalls<AnalysisPromptCategory>>> setPropertyCalls,
        Expression<Func<AnalysisPromptCategory, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    );
}
