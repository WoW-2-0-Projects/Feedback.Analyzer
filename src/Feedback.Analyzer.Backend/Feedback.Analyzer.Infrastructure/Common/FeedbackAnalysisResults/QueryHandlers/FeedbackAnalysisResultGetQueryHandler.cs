using AutoMapper;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Queries;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.FeedbackAnalysisResults.QueryHandlers;

/// <summary>
/// Represents a handler class responsible for processing the `FeedbackAnalysisResultGetQuery` query.
/// </summary>
public class FeedbackAnalysisResultGetQueryHandler
    (IMapper mapper, IFeedbackAnalysisResultService feedbackAnalysisResultService)
    : IQueryHandler<FeedbackAnalysisResultGetQuery, ICollection<FeedbackAnalysisResultDto>>
{
    public async Task<ICollection<FeedbackAnalysisResultDto>> Handle(FeedbackAnalysisResultGetQuery request, CancellationToken cancellationToken)
    {
        var matchedPromptsFeedbackAnalysisResults =  await feedbackAnalysisResultService.Get(request.Filter, new QueryOptions
            { AsNoTracking = true }).ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<FeedbackAnalysisResultDto>>(matchedPromptsFeedbackAnalysisResults);
    }
}