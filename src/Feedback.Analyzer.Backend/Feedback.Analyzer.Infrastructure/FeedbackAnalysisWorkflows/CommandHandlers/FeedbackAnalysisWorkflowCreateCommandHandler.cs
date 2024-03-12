using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.CommandHandlers;

/// <summary>
/// Represents a command handler for creating a feedback analysis workflow.
/// </summary>
public class FeedbackAnalysisWorkflowCreateCommandHandler(
    IMapper mapper,
    FeedbackAnalysisWorkflowProcessingService feedbackAnalysisWorkflowProcessingService)
    : ICommandHandler<FeedbackAnalysisWorkflowCreateCommand, FeedbackAnalysisWorkflowDto>
{
    public async Task<FeedbackAnalysisWorkflowDto> Handle(FeedbackAnalysisWorkflowCreateCommand request,
        CancellationToken cancellationToken)
    {
        var feedbackAnalysisWorkflow = mapper.Map<FeedbackAnalysisWorkflow>(request.FeedbackAnalysisWorkflow);
        var analysisWorkflow = mapper.Map<AnalysisWorkflow>(request.FeedbackAnalysisWorkflow);
        
        var updatedWorkflow =
            await feedbackAnalysisWorkflowProcessingService.CreateAsync(feedbackAnalysisWorkflow, analysisWorkflow,
                cancellationToken);

        var createdWorkflow = mapper.Map<FeedbackAnalysisWorkflowDto>(updatedWorkflow.FeedbackAnalysisWorkflow);
        mapper.Map(updatedWorkflow.AnalysisWorkflow, createdWorkflow);

        return createdWorkflow;
    }
}