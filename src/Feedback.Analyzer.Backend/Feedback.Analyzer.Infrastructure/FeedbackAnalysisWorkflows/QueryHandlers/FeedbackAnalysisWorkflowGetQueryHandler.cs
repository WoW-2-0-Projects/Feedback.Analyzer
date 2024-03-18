using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.QueryHandlers;

/// <summary>
/// Represents a query handler for retrieving feedback analysis workflows.
/// </summary>
public class FeedbackAnalysisWorkflowGetQueryHandler(
    IMapper mapper,
    IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService,
    IRequestContextProvider requestContextProvider
) : IQueryHandler<FeedbackAnalysisWorkflowGetQuery, ICollection<FeedbackAnalysisWorkflowDto>>
{
    public async Task<ICollection<FeedbackAnalysisWorkflowDto>> Handle(FeedbackAnalysisWorkflowGetQuery request, CancellationToken cancellationToken)
    {
        request.Filter.ClientId = requestContextProvider.GetUserId();
        var queryOptions = new QueryOptions(QueryTrackingMode.AsNoTracking);

        var matchedFeedbackAnalysisWorkflows = await (await feedbackAnalysisWorkflowService
                .Get(request.Filter, queryOptions)
                .GetFilteredEntitiesQuery(feedbackAnalysisWorkflowService.Get(), cancellationToken: cancellationToken))
            .Include(workflow => workflow.AnalysisWorkflow)
            .Include(workflow => workflow.Product)
            .ApplyTrackingMode(queryOptions.TrackingMode)
            .ProjectTo<FeedbackAnalysisWorkflowDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);

        return matchedFeedbackAnalysisWorkflows;
    }
}