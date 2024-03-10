using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.Common.FeedbackAnalysisResults.Services;

/// <summary>
/// Represents a service for managing feedback analysis results.
/// </summary>
public class FeedbackAnalysisResultService(IFeedbackAnalysisResultRepository feedbackAnalysisResultRepository) : IFeedbackAnalysisResultService
{
    public IQueryable<FeedbackAnalysisResult> Get(
        Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default,
        QueryOptions queryOptions = default
    )
    {
        return feedbackAnalysisResultRepository.Get(predicate, queryOptions);
    }

    public IQueryable<FeedbackAnalysisResult> Get(
        FeedbackAnalysisResultFilter feedbackAnalysisResultFilter, 
        QueryOptions queryOptions = default) =>
        feedbackAnalysisResultRepository.Get(queryOptions: queryOptions).ApplyPagination(feedbackAnalysisResultFilter);

    public ValueTask<FeedbackAnalysisResult?> GetByIdAsync(
        Guid feedbackAnalysisResultId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisResultRepository.GetByIdAsync(feedbackAnalysisResultId, queryOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisResult> CreateAsync(
        FeedbackAnalysisResult analysisResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisResultRepository.CreateAsync(analysisResult, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisResult?> DeleteByIdAsync(
        Guid analysisResultId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisResultRepository.DeleteByIdAsync(analysisResultId, commandOptions, cancellationToken);
}