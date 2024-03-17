using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.CommandHandlers;

/// <summary>
/// Represents a command handler for creating a feedback analysis workflow.
/// </summary>
public class FeedbackAnalysisWorkflowCreateCommandHandler(
    IMapper mapper,
    IFeedbackAnalysisWorkflowProcessingService feedbackAnalysisWorkflowProcessingService
) : ICommandHandler<FeedbackAnalysisWorkflowCreateCommand, FeedbackAnalysisWorkflowDto>
{
    public async Task<FeedbackAnalysisWorkflowDto> Handle(FeedbackAnalysisWorkflowCreateCommand request, CancellationToken cancellationToken)
    {
        var feedbackAnalysisWorkflow = mapper.Map<FeedbackAnalysisWorkflow>(request.FeedbackAnalysisWorkflow);
        var analysisWorkflow = mapper.Map<AnalysisWorkflow>(request.FeedbackAnalysisWorkflow);
        
        // TODO : Add product Id on frontend and workflow type based on client role
        var createdWorkflow = await feedbackAnalysisWorkflowProcessingService.CreateAsync(
            feedbackAnalysisWorkflow,
            analysisWorkflow,
            request.BaseWorkflowId,
            cancellationToken
        );

        var result = mapper.Map<FeedbackAnalysisWorkflowDto>(createdWorkflow.FeedbackAnalysisWorkflow);
        mapper.Map(createdWorkflow.AnalysisWorkflow, result);

        return result;
    }
}