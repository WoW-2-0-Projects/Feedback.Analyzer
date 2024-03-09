using Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflows.Services;

/// <summary>
/// Interface for managing FeedbackAnalysisWorkflow entities.
/// </summary>
public class AnalysisWorkflowService(IAnalysisWorkflowsRepository analysisWorkflowsRepository) : IAnalysisWorkflowService
{
    public async ValueTask<bool> UpdateStatus(Guid workflowId, WorkflowStatus status, CancellationToken cancellationToken = default)
    {
        var updatedWorkflowsCount =  await analysisWorkflowsRepository.UpdateBatchAsync(
            setPropertyCalls => setPropertyCalls.SetProperty(workflow => workflow.Status, status),
            workflow => workflow.Id == workflowId,
            cancellationToken
        );

        return updatedWorkflowsCount == 1;
    }
}