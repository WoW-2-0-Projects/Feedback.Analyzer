using System.Collections.Immutable;
using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

public interface IPromptCategoryRepository
{
    IQueryable<AnalysisPromptCategory> Get(
        Expression<Func<AnalysisPromptCategory, bool>>? predicate = default, 
        QueryOptions queryOptions = default);
    
     ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<AnalysisPromptCategory>, SetPropertyCalls<AnalysisPromptCategory>>> setPropertyCalls,
        Expression<Func<AnalysisPromptCategory, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    );
}