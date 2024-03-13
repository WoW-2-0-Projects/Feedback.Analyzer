using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.QueryHandlers;

/// <summary>
/// Represents a query handler for retrieving feedback analysis workflows.
/// </summary>
public class FeedbackAnalysisWorkflowGetQueryHandler(IMapper mapper, IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService)
    : IQueryHandler<FeedbackAnalysisWorkflowGetQuery, ICollection<FeedbackAnalysisWorkflowDto>>
{
    public async Task<ICollection<FeedbackAnalysisWorkflowDto>> Handle(FeedbackAnalysisWorkflowGetQuery request, CancellationToken cancellationToken)
    {
        var matchedFeedbackAnalysisWorkflows = await feedbackAnalysisWorkflowService.Get(
            request.Filter,
            new QueryOptions
            {
                AsNoTracking = true
            }).ToListAsync(cancellationToken);
        return mapper.Map<ICollection<FeedbackAnalysisWorkflowDto>>(matchedFeedbackAnalysisWorkflows);
    }
}