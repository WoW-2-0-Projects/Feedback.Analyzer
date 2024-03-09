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
    : EntityRepositoryBase<WorkflowExecutionOption, AppDbContext>(dbContext), IWorkflowExecutionOptionRepository
{
    public async ValueTask<WorkflowExecutionOption?> GetByIdAndIncludeAllGrandChildrenAsync(
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
            executionOption.ChildExecutionOptions = await LoadAllChildrenAsync2(executionOption.Id, queryOptions, cancellationToken);

        return executionOption;
    }

    // /// <summary>
    // /// Loads all children of the given execution option
    // /// </summary>
    // /// <param name="executionOption">Execution option to load children for</param>
    // /// <param name="cancellationToken">Cancellation token</param>
    // private async ValueTask LoadAllChildrenAsync(WorkflowExecutionOption executionOption, CancellationToken cancellationToken = default)
    // {
    //     await DbContext.Entry(executionOption).Reference(option => option.AnalysisPromptCategory!).LoadAsync(cancellationToken);
    //     await DbContext.Entry(executionOption).Reference(option => option.AnalysisPromptCategory!.SelectedPrompt).LoadAsync(cancellationToken);
    //
    //     if (executionOption.ChildExecutionOptions is null || executionOption.ChildExecutionOptions.Count == 0)
    //         return;
    //
    //     foreach (var childOption in executionOption.ChildExecutionOptions)
    //     {
    //         await DbContext.Entry(childOption).Reference(option => option.AnalysisPromptCategory!).LoadAsync(cancellationToken);
    //         await DbContext.Entry(childOption).Collection(option => option.ChildExecutionOptions!).LoadAsync(cancellationToken);
    //         await DbContext.Entry(executionOption).Reference(option => option.AnalysisPromptCategory!.SelectedPrompt).LoadAsync(cancellationToken);
    //
    //         if (childOption.ChildExecutionOptions is not null && childOption.ChildExecutionOptions.Count > 0)
    //             await LoadAllChildrenAsync(childOption, cancellationToken);
    //     }
    // }

    /// <summary>
    /// Loads all children of the given execution option
    /// </summary>
    /// <param name="parentOptionId"></param>
    /// <param name="queryOptions"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    private async ValueTask<IReadOnlyList<WorkflowExecutionOption>> LoadAllChildrenAsync2(
        Guid parentOptionId,
        QueryOptions queryOptions,
        CancellationToken cancellationToken = default
    )
    {
        // Load children
        var childrenOptions = await Get(executionOption => executionOption.ParentId == parentOptionId, queryOptions)
            .Include(executionOption => executionOption.AnalysisPromptCategory)
                .ThenInclude(category => category.SelectedPrompt)
            .Include(executionOption => executionOption.ChildExecutionOptions)
            .AsSplitQuery()
            .ToListAsync(cancellationToken: cancellationToken);

        // Load grand children
        await Task.WhenAll(
            childrenOptions.Select(
                async childrenOption => { childrenOption.ChildExecutionOptions = await LoadAllChildrenAsync2(childrenOption.Id, queryOptions, cancellationToken); }
            )
        );
        
        return childrenOptions;
    }
}