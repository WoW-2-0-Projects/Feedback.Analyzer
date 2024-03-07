using AutoMapper;
using Feedback.Analyzer.Application.AnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Application.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.AnalysisWorkflows.CommandHandlers;

/// <summary>
/// Handles the command for creating an analysis workflow.
/// </summary>
public class AnalysisWorkflowCreateCommandHandler(IMapper mapper, IAnalysisWorkflowService analysisWorkflowService)
    : ICommandHandler<AnalysisWorkflowCreateCommand, AnalysisWorkflowDto>
{
    public async Task<AnalysisWorkflowDto> Handle(AnalysisWorkflowCreateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var analysisWorkflow = mapper.Map<AnalysisWorkflow>(request.AnalysisWorkflow);
        
        // call service
        var createAnalysisWorkflow = await analysisWorkflowService.CreateAsync(analysisWorkflow, cancellationToken: cancellationToken);
        
        // Conversation to Dto
        var analysisWorkflowDto = mapper.Map<AnalysisWorkflowDto>(createAnalysisWorkflow);

        return analysisWorkflowDto;
    }
}