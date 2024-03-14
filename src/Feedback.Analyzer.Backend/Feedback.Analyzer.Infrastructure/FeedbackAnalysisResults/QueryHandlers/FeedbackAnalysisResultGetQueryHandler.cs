using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.QueryHandlers;

/// <summary>
/// Handles the query to retrieve feedback analysis results.
/// </summary>
public class FeedbackAnalysisResultGetQueryHandler(IMapper mapper, IFeedbackAnalysisResultService feedbackAnalysisResultService)
    : IQueryHandler<FeedbackAnalysisResultGetQuery,ICollection<FeedbackAnalysisResultDto>>
{
    public async Task<ICollection<FeedbackAnalysisResultDto>> Handle(FeedbackAnalysisResultGetQuery request, CancellationToken cancellationToken)
    {
        var matchedFeedbackAnalysisResult =
            await feedbackAnalysisResultService.Get(request.Filter, new QueryOptions() { AsNoTracking = true })
                .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<FeedbackAnalysisResultDto>>(matchedFeedbackAnalysisResult);
    }
}