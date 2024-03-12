using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.CommandHandlers;

public class FeedbackAnalysisWorkflowUpdateCommandHandler(
    IMapper mapper,
    FeedbackAnalysisWorkflowProcessingService feedbackAnalysisWorkflowProcessingService)
    : ICommandHandler<FeedbackAnalysisWorkflowUpdateCommand, FeedbackAnalysisWorkflowDto>
{
    public async Task<FeedbackAnalysisWorkflowDto> Handle(FeedbackAnalysisWorkflowUpdateCommand request, CancellationToken cancellationToken)
    {
        var feedbackAnalysisWorkflow = mapper.Map<FeedbackAnalysisWorkflow>(request.FeedbackAnalysisWorkflow);
        var analysisWorkflow = mapper.Map<AnalysisWorkflow>(request.FeedbackAnalysisWorkflow);
        
        var createFeedbackAnalysisWorkflow =
            await feedbackAnalysisWorkflowProcessingService.UpdateAsync(feedbackAnalysisWorkflow, analysisWorkflow , cancellationToken: cancellationToken);
        
        var updateWorkflow = mapper.Map<FeedbackAnalysisWorkflowDto>(createFeedbackAnalysisWorkflow.FeedbackAnalysisWorkflow);
        mapper.Map(createFeedbackAnalysisWorkflow.AnalysisWorkflow, updateWorkflow);
        
        return updateWorkflow;
    }
}