using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Modal;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Query;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.QueryHandlers;

/// <summary>
/// Handles the query to retrieve feedback analysis workflow results by workflow ID.
/// </summary>
public class FeedbackWorkflowResultGetQueryHandler(
    IMapper mapper,
    IFeedbackAnalysisWorkflowResultService feedbackAnalysisWorkflowResultService
) : IQueryHandler<FeedbackWorkflowResultGetQuery, ICollection<FeedbackAnalysisWorkflowResultDto>>
{
    public async Task<ICollection<FeedbackAnalysisWorkflowResultDto>> Handle(
        FeedbackWorkflowResultGetQuery request,
        CancellationToken cancellationToken
    )
    {
        var queryOptions = new QueryOptions
        {
            TrackingMode = QueryTrackingMode.AsNoTrackingWithIdentityResolution
        };

        var matchedWorkflowResults = await feedbackAnalysisWorkflowResultService
            .Get(workflowResult => !request.WorkflowId.HasValue || workflowResult.WorkflowId == request.WorkflowId.Value)
            // .Include(workflowResult => workflowResult.FeedbackAnalysisResults)
            .ApplyTrackingMode(queryOptions.TrackingMode)
            .ProjectTo<FeedbackAnalysisWorkflowResultDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return matchedWorkflowResults;
    }
}