using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var resultsQuery = feedbackAnalysisResultRepository
            .Get(queryOptions: queryOptions)
            .Include(result => result.CustomerFeedback)
            .AsQueryable();
        
        if(feedbackAnalysisResultFilter.ResultId.HasValue)
            resultsQuery = resultsQuery.Where(result => result.FeedbackAnalysisWorkflowResultId == feedbackAnalysisResultFilter.ResultId.Value);
        
        return resultsQuery.ApplyPagination(feedbackAnalysisResultFilter);
    }

    public ValueTask<FeedbackAnalysisResult?> GetByIdAsync(Guid feedbackId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return feedbackAnalysisResultRepository.GetByIdAsync(feedbackId, queryOptions, cancellationToken);
    }

    public ValueTask<FeedbackAnalysisResult> CreateAsync(FeedbackAnalysisResult feedbackAnalysisResult, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return feedbackAnalysisResultRepository.CreateAsync(feedbackAnalysisResult, commandOptions, cancellationToken);
    }
}