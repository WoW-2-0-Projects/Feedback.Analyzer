using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a feedback analysis result by workflow result ID.
/// </summary>
public class FeedbackAnalysisResultGetByIdQueryHandler(IMapper mapper, IFeedbackAnalysisResultService feedbackAnalysisResultService) 
    : IQueryHandler<FeedbackAnalysisResultGetByWorkflowResultIdQuery, FeedbackAnalysisResultDto?>
{
    public async Task<FeedbackAnalysisResultDto?> Handle(FeedbackAnalysisResultGetByWorkflowResultIdQuery request, CancellationToken cancellationToken)
    {
        var foundFeedbackAnalysisResult = await feedbackAnalysisResultService.GetByIdAsync(
            request.WorkflowResultId,
            new QueryOptions()
            {
                TrackingMode = QueryTrackingMode.AsNoTracking,
            },
            cancellationToken
        );

        return mapper.Map<FeedbackAnalysisResultDto>(foundFeedbackAnalysisResult);
    }
}