using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.QueryHandlers;

/// <summary>
/// Handles the query to retrieve feedback analysis results.
/// </summary>
public class FeedbackAnalysisResultGetByIdQueryHandler(IMapper mapper, IFeedbackAnalysisResultService feedbackAnalysisResultService)
    : IQueryHandler<FeedbackAnalysisResultGetByIdQuery, FeedbackAnalysisResultDto?>
{
    public async Task<FeedbackAnalysisResultDto> Handle(FeedbackAnalysisResultGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundResult = await feedbackAnalysisResultService.Get(
                result => result.Id == request.ResultId,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .ProjectTo<FeedbackAnalysisResultDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return foundResult;
    }
}