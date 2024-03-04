using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

public interface IPromptCategoryRepository
{
    IQueryable<AnalysisPromptCategory> Get(
        Expression<Func<AnalysisPromptCategory, bool>>? predicate = default, 
        QueryOptions queryOptions = default);
}