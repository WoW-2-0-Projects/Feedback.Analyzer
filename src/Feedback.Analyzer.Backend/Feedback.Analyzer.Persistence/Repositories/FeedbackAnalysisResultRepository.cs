using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository class for managing feedback analysis results.
/// </summary>
public class FeedbackAnalysisResultRepository(AppDbContext appDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<FeedbackAnalysisResult, AppDbContext>(appDbContext, cacheBroker), IFeedbackAnalysisResultRepository
{
    public new IQueryable<FeedbackAnalysisResult> Get(Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<FeedbackAnalysisResult?> GetByIdAsync(Guid feedbackId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(feedbackId, queryOptions, cancellationToken);
    }

    public new ValueTask<FeedbackAnalysisResult> CreateAsync(FeedbackAnalysisResult feedbackAnalysisResult, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(feedbackAnalysisResult, commandOptions, cancellationToken);
    }

    public new ValueTask<FeedbackAnalysisResult?> DeleteByIdAsync(Guid feedbackId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(feedbackId, commandOptions, cancellationToken);
    }
}