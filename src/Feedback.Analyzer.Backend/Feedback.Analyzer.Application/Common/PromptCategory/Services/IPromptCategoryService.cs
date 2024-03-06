using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.PromptCategory.Services;

/// <summary>
/// Interface for accessing prompt category data.
/// </summary>
public interface IPromptCategoryService
{
    /// <summary>
    /// Retrieves analysis prompt categories based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate to filter the analysis prompt categories.</param>
    /// <param name="queryOptions">The query options for sorting and paging.</param>
    /// <returns>An IQueryable of AnalysisPromptCategory.</returns>
    IQueryable<AnalysisPromptCategory> Get(
        Expression<Func<AnalysisPromptCategory, bool>>? predicate = default,
        QueryOptions queryOptions = default
    );

    /// <summary>
    /// Retrieves analysis prompt categories based on the specified prompt category filter and query options.
    /// </summary>
    /// <param name="promptCategoryFilter">The prompt category filter to apply.</param>
    /// <param name="queryOptions">The query options for sorting and paging.</param>
    /// <returns>An IQueryable of AnalysisPromptCategory.</returns>
    IQueryable<AnalysisPromptCategory> Get(
        PromptCategoryFilter promptCategoryFilter, 
        QueryOptions queryOptions = default
    );
}