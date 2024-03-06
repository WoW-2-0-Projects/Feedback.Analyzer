using AutoMapper;
using Feedback.Analyzer.Application.FeedbackWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.CommandHandlers;

/// <summary>
/// Represents a command handler responsible for creating a feedback workflow.
/// </summary>
public  class FeedbackWorkflowCreateCommandHandler(IMapper mapper, IFeedbackWorkflowService feedbackWorkflowService):
    ICommandHandler<FeedbackWorkflowCreateCommand, FeedbackWorkflowDto>
{
    public async Task<FeedbackWorkflowDto> Handle(FeedbackWorkflowCreateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var feedbackWorkflow = mapper.Map<FeedbackWorkflow>(request.FeedbackWorkflow);

        // Call service
        var createFeedbackWorkflow =
            await feedbackWorkflowService.CreateAsync(feedbackWorkflow, cancellationToken: cancellationToken);

        // Conversion to DTO
        var feedbackWorkflowDto = mapper.Map<FeedbackWorkflowDto>(createFeedbackWorkflow);

        return feedbackWorkflowDto;
    }
}