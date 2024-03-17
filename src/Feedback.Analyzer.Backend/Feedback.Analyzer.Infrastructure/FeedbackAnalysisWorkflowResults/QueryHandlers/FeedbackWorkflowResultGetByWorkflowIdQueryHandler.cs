using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Modal;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Query;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.QueryHandlers;

public class FeedbackWorkflowResultGetByWorkflowIdQueryHandler(
    IMapper mapper,
    IFeedbackAnalysisWorkflowResultService feedbackAnalysisWorkflowResultService
) : IQueryHandler<FeedbackWorkflowResultGetByWorkflowIdQuery, ICollection<FeedbackAnalysisWorkflowResultDto>>
{
    public async Task<ICollection<FeedbackAnalysisWorkflowResultDto>> Handle(
        FeedbackWorkflowResultGetByWorkflowIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var queryOptions = new QueryOptions
        {
            TrackingMode = QueryTrackingMode.AsNoTrackingWithIdentityResolution
        };

        var matchedWorkflowResults = await feedbackAnalysisWorkflowResultService
            .Get(workflowResult => workflowResult.WorkflowId == request.WorkflowId)
            .Include(workflowResult => workflowResult.FeedbackAnalysisResults)
            .ApplyTrackingMode(queryOptions.TrackingMode)
            .ProjectTo<FeedbackAnalysisWorkflowResultDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return matchedWorkflowResults;
    }
}