using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Provides workflow execution options repository functionality
/// </summary>
public class WorkflowExecutionOptionRepository(AppDbContext dbContext)
    : EntityRepositoryBase<WorkflowExecutionOptions, AppDbContext>(dbContext), IWorkflowExecutionOptionRepository
{
    public async ValueTask<WorkflowExecutionOptions?> GetByIdAndIncludeAllGrandChildrenAsync(
        Guid optionId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var executionOption = await Get(executionOption => executionOption.Id == optionId, queryOptions)
            .Include(executionOption => executionOption.AnalysisPromptCategory)
            .Include(executionOption => executionOption.ChildExecutionOptions)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        if (executionOption is not null)
            await LoadAllChildrenAsync(executionOption, cancellationToken);

        return executionOption;
    }

    private async ValueTask LoadAllChildrenAsync(WorkflowExecutionOptions executionOption, CancellationToken cancellationToken = default)
    {
        if (executionOption.ChildExecutionOptions is null || executionOption.ChildExecutionOptions.Count == 0)
            return;

        // Load all children execution options and category references
        var loadChildrenTasks = executionOption.ChildExecutionOptions.Select(
            async childOption =>
            {
                await DbContext.Entry(childOption).Collection(option => option.ChildExecutionOptions!).LoadAsync(cancellationToken);
                await DbContext.Entry(childOption).Reference(option => option.AnalysisPromptCategory!).LoadAsync(cancellationToken);
            }
        );

        await Task.WhenAll(loadChildrenTasks);
    }
}