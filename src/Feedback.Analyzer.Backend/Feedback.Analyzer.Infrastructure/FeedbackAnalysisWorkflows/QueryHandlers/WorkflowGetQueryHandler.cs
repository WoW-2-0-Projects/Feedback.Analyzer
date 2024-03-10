using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.QueryHandlers;

public class WorkflowGetQueryHandler(IMapper mapper, IFeedbackAnalysisWorkflowService workflowService)
    : IQueryHandler<FeedbackWorkflowGetQuery, ICollection<FeedbackExecutionWorkflowDto>>
{
    public async Task<ICollection<FeedbackExecutionWorkflowDto>> Handle(FeedbackWorkflowGetQuery request, CancellationToken cancellationToken)
    {
        var workflows = await workflowService.Get(
                feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.Workflow.Type == request.Filter.Type,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .Include(feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.Workflow)
            .ApplyPagination(request.Filter)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<ICollection<FeedbackExecutionWorkflowDto>>(workflows);
    }
}