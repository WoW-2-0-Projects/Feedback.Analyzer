using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a repository for managing feedback analysis results in the persistence layer.
/// </summary>
/// <param name="appDbContext"></param>
public class FeedbackAnalysisResultRepository(AppDbContext appDbContext)
    : EntityRepositoryBase<FeedbackAnalysisResult, AppDbContext>(appDbContext),
        IFeedbackAnalysisResultRepository
{
    public new IQueryable<FeedbackAnalysisResult> Get(Expression<Func<FeedbackAnalysisResult, bool>>? 
        predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<FeedbackAnalysisResult?> GetByIdAsync(Guid feedbackAnalysisResultId, 
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(feedbackAnalysisResultId, queryOptions, cancellationToken);
    }
}