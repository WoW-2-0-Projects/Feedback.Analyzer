using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.QueryHandlers;

/// <summary>
/// Represents a query handler for retrieving a feedback analysis workflow by its ID.
/// </summary>
public class FeedbackAnalysisWorkflowGetByIdQueryHandler(
    IMapper mapper,
    IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService)
    : IQueryHandler<FeedbackAnalysisWorkflowGetByIdQuery, FeedbackAnalysisWorkflowDto>
{
    public async Task<FeedbackAnalysisWorkflowDto> Handle(FeedbackAnalysisWorkflowGetByIdQuery request,
        CancellationToken cancellationToken)
    {
        var foundFeedbackAnalysisWorkflow = await feedbackAnalysisWorkflowService.Get
        (
            workflow => workflow.Id == request.Id,
            new QueryOptions
            {
                AsNoTracking = true
            }
        )
        .Include(workflow => workflow.AnalysisWorkflow)
        .FirstOrDefaultAsync(cancellationToken);
        return mapper.Map<FeedbackAnalysisWorkflowDto>(foundFeedbackAnalysisWorkflow);
    }
}