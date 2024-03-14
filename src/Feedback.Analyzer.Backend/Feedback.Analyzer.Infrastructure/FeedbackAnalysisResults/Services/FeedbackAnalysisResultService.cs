using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.Services;

/// <summary>
/// Implementation of the feedback analysis result service.
/// </summary>
public class FeedbackAnalysisResultService(IFeedbackAnalysisResultRepository feedbackAnalysisResultRepository) : IFeedbackAnalysisResultService
{
    public IQueryable<FeedbackAnalysisResult> Get(Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return feedbackAnalysisResultRepository.Get(predicate, queryOptions);
    }

    public IQueryable<FeedbackAnalysisResult> Get(FeedbackAnalysisResultFilter feedbackAnalysisResultFilter, QueryOptions queryOptions = default)
    {
        return feedbackAnalysisResultRepository.Get(queryOptions: queryOptions)
            .ApplyPagination(feedbackAnalysisResultFilter);
    }

    public ValueTask<FeedbackAnalysisResult?> GetByIdAsync(Guid feedbackId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return feedbackAnalysisResultRepository.GetByIdAsync(feedbackId, queryOptions, cancellationToken);
    }
}