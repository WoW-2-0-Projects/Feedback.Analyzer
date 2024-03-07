using AutoMapper;
using Feedback.Analyzer.Application.AnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Application.AnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.AnalysisWorkflows.CommandHandlers;

/// <summary>
/// Handles the command for updating an analysis workflow.
/// </summary>
public class AnalysisWorkflowUpdateCommandHandler(IMapper mapper, IAnalysisWorkflowService analysisWorkflowService)
    : ICommandHandler<AnalysisWorkflowUpdateCommand, AnalysisWorkflowDto>
{
    public async Task<AnalysisWorkflowDto> Handle(AnalysisWorkflowUpdateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var analysisWorkflow = mapper.Map<AnalysisWorkflow>(request.AnalysisWorkflow);
        
        // call service
        var createAnalysisWorkflow = await analysisWorkflowService.UpdateAsync(analysisWorkflow, cancellationToken: cancellationToken);
        
        // Conversation to Dto
        var analysisWorkflowDto = mapper.Map<AnalysisWorkflowDto>(createAnalysisWorkflow);

        return analysisWorkflowDto;
    }
}