using AutoMapper;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackWorkflows.Queries;
using Feedback.Analyzer.Application.FeedbackWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.QueryHandlers;

/// <summary>
/// Represents a query handler responsible for retrieving a feedback workflow by its ID.
/// </summary>
public class FeedbackWorkflowGetByIdQueryHandler(IFeedbackWorkflowService feedbackWorkflowService, IMapper mapper)
: IQueryHandler<FeedbackWorkflowGetByIdQuery, FeedbackWorkflowDto>
{
    public async Task<FeedbackWorkflowDto> Handle(FeedbackWorkflowGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundFeedbackWorkflow = await feedbackWorkflowService.GetByIdAsync(
            request.FeedbackWorkflowId,
            new QueryOptions()
            {
                AsNoTracking = true
            }, cancellationToken
        );
        
        return mapper.Map<FeedbackWorkflowDto>(foundFeedbackWorkflow);
    }
}