using AutoMapper;
using Feedback.Analyzer.Application.AnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.AnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.AnalysisWorkflows.CommandHandlers;

/// <summary>
/// Handles the command for deleting an analysis workflow by its ID.
/// </summary>
public class AnalysisWorkflowDeleteByIdCommandHandler(IMapper mapper, IAnalysisWorkflowService analysisWorkflowService)
  : ICommandHandler<AnalysisWorkflowDeleteByIdCommand, bool>
{
  public  async Task<bool> Handle(AnalysisWorkflowDeleteByIdCommand request, CancellationToken cancellationToken)
  {
      // Delete the analysis workflow by its ID using the service
      await analysisWorkflowService.DeleteByIdAsync(request.AnalysisWorkflowId, cancellationToken: cancellationToken);
      
      // Return true indicating the deletion was successful
      return true;
  }
}