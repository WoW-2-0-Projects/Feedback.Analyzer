using AutoMapper;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Queries;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.Common.FeedbackAnalysisResults.QueryHandlers;

/// <summary>
/// Represents a handler class responsible for processing the `FeedbackAnalysisResultGetByIdQuery` query.
/// </summary>
public class FeedbackAnalysisResultGetByIdQueryHandler(IFeedbackAnalysisResultService feedbackAnalysisResultService, IMapper mapper)
    : IQueryHandler<FeedbackAnalysisResultGetByIdQuery, FeedbackAnalysisResultDto?>
{
    public async Task<FeedbackAnalysisResultDto?> Handle(FeedbackAnalysisResultGetByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await feedbackAnalysisResultService.GetByIdAsync(request.FeedbackAnalysisResultId,
            new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking }, cancellationToken);

        return mapper.Map<FeedbackAnalysisResultDto>(result);
    }
}