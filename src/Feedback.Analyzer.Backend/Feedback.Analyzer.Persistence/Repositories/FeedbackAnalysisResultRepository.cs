using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Provides feedback analysis result repository functionality
/// </summary>
public class FeedbackAnalysisResultRepository(AppDbContext appDbContext)
    : EntityRepositoryBase<FeedbackAnalysisResult, AppDbContext>(appDbContext), IFeedbackAnalysisResultRepository
{
    public new IQueryable<FeedbackAnalysisResult> Get(
        Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        base.Get(predicate, queryOptions);

    public new ValueTask<FeedbackAnalysisResult?> GetByIdAsync(
        Guid feedbackAnalysisResultId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.GetByIdAsync(feedbackAnalysisResultId, queryOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisResult> CreateAsync(
        FeedbackAnalysisResult result,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.CreateAsync(result, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisResult?> DeleteByIdAsync(
        Guid resultId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.DeleteByIdAsync(resultId, commandOptions, cancellationToken);
}