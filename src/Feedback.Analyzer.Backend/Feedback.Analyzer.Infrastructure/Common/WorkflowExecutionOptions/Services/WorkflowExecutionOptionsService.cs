using Feedback.Analyzer.Application.Common.WorkflowExecutionOptions.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.Common.WorkflowExecutionOptions.Services;

/// <summary>
/// Defines workflow execution options service.
/// </summary>
public class WorkflowExecutionOptionsService(IWorkflowExecutionOptionRepository workflowExecutionOptionRepository) : IWorkflowExecutionOptionsService
{
    public ValueTask<WorkflowExecutionOption?> GetByIdForExecutionAsync(
        Guid executionOptionsId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        workflowExecutionOptionRepository.GetByIdAndIncludeChildrenAndPromptAsync(executionOptionsId, queryOptions, cancellationToken);

    public ValueTask<WorkflowExecutionOption> GetByIdForCloningAsync(
        Guid executionOptionsId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>  workflowExecutionOptionRepository.GetByIdAndIncludeChildren(executionOptionsId, queryOptions, cancellationToken);
}