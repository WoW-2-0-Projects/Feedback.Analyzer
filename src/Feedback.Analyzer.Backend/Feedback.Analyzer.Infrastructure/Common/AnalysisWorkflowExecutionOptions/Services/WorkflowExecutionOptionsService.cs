using Feedback.Analyzer.Application.Common.AnalysisWorkflowExecutionOptions.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflowExecutionOptions.Services;

/// <summary>
/// Defines workflow execution options service.
/// </summary>
public class WorkflowExecutionOptionsService(IWorkflowExecutionOptionRepository workflowExecutionOptionRepository) : IWorkflowExecutionOptionsService
{
    public ValueTask<WorkflowExecutionOptions?> GetByIdForExecutionAsync(
        Guid executionOptionsId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        workflowExecutionOptionRepository.GetByIdAndIncludeAllGrandChildrenAsync(executionOptionsId, queryOptions, cancellationToken);
}