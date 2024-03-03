using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.PromptCategories.Models;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.PromptCategories.Services;

public interface IPromptCategoryService
{
    IQueryable<AnalysisPromptCategory> Get(
        Expression<Func<AnalysisPromptCategory, bool>>? predicate = default,
        QueryOptions queryOptions = default
    );

    IQueryable<AnalysisPromptCategory> Get(
        PromptCategoryFilter promptCategoryFilter, 
        QueryOptions queryOptions = default);
}