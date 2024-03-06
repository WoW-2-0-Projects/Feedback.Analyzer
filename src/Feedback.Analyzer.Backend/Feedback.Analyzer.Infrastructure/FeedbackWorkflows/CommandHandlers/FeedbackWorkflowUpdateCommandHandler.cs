using AutoMapper;
using Feedback.Analyzer.Application.FeedbackWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.CommandHandlers;

/// <summary>
/// Represents a command handler responsible for updating a feedback workflow.
/// </summary>
public class FeedbackWorkflowUpdateCommandHandler(IMapper mapper, IFeedbackWorkflowService feedbackWorkflowService)
: ICommandHandler<FeedbackWorkflowUpdateCommand, FeedbackWorkflowDto>
{
    public async Task<FeedbackWorkflowDto> Handle(FeedbackWorkflowUpdateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var feedbackWorkflow = mapper.Map<FeedbackWorkflow>(request.FeedbackWorkflow);

        // Call service
        var updateFeedbackWorkflow =
            await feedbackWorkflowService.UpdateAsync(feedbackWorkflow, cancellationToken: cancellationToken);

        // Conversion to DTO
        var feedbackWorkflowDto = mapper.Map<FeedbackWorkflowDto>(updateFeedbackWorkflow);

        return feedbackWorkflowDto;
    }
}