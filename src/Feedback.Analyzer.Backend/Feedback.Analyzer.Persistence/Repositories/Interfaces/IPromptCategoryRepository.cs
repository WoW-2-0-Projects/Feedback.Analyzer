using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

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
}
