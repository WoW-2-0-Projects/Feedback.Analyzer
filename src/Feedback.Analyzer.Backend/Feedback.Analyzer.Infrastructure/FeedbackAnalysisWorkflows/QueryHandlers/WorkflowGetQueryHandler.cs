using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.QueryHandlers;

public class WorkflowGetQueryHandler(IMapper mapper, IFeedbackAnalysisWorkflowService workflowService)
    : IQueryHandler<WorkflowGetQuery, ICollection<FeedbackExecutionWorkflowDto>>
{
    public async Task<ICollection<FeedbackExecutionWorkflowDto>> Handle(WorkflowGetQuery request, CancellationToken cancellationToken)
    {
        var workflows = await workflowService.Get(
                workflow => workflow.Type == request.Filter.Type,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .ApplyPagination(request.Filter)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<ICollection<FeedbackExecutionWorkflowDto>>(workflows);
    }
}