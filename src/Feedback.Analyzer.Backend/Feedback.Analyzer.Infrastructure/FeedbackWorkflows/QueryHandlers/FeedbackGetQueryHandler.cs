using AutoMapper;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackWorkflows.Queries;
using Feedback.Analyzer.Application.FeedbackWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.QueryHandlers;

/// <summary>
/// Represents a query handler responsible for retrieving feedback workflows.
/// </summary>
public class FeedbackGetQueryHandler(IFeedbackWorkflowService feedbackWorkflowService, IMapper mapper): IQueryHandler<FeedbackWorkflowGetQuery, ICollection<FeedbackWorkflowDto>>
{
    public async Task<ICollection<FeedbackWorkflowDto>> Handle(FeedbackWorkflowGetQuery feedbackWorkflowGetQuery, CancellationToken cancellationToken)
    {
        var matchedFeedbackWorkflow = await feedbackWorkflowService
            .Get(feedbackWorkflowGetQuery.Filter, new QueryOptions{AsNoTracking = true} )
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<FeedbackWorkflowDto>>(matchedFeedbackWorkflow);
    }
}