using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository class for managing workflow execution options.
/// </summary>
public class WorkflowExecutionOptionRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<WorkflowExecutionOption, AppDbContext>(dbContext, cacheBroker),
      IWorkflowExecutionOptionRepository
{
    public async ValueTask<WorkflowExecutionOption> GetByIdAndIncludeChildrenAndPromptAsync(
        Guid optionId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        // Includes all children and prompts
        async ValueTask LoadAllChildrenAsync(
            Guid parentId,
            QueryOptions localQueryOptions,
            CancellationToken localCancellationToken)
        {
            var childOptions = await Get(executionOption => executionOption.ParentId == parentId, localQueryOptions)
                .Include(executionOption => executionOption.AnalysisPromptCategory)
                .ThenInclude(category => category.SelectedPrompt)
                .Include(executionOption => executionOption.ChildExecutionOptions)
                .AsSplitQuery()
                .ToListAsync(localCancellationToken);
        
            foreach(var childOption in childOptions)
            {
                childOption.AnalysisPromptCategory.SelectedPrompt!.Category = childOption.AnalysisPromptCategory;
                await LoadAllChildrenAsync(childOption.Id, localQueryOptions, localCancellationToken);
            }
        }

        // Main method logic
        var executionOption = await Get(executionOption => executionOption.Id == optionId, queryOptions)
            .Include(executionOption => executionOption.ChildExecutionOptions)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        if (executionOption is not null)
            await LoadAllChildrenAsync(executionOption.Id, queryOptions, cancellationToken);
    
        return executionOption;
    }

    public async ValueTask<WorkflowExecutionOption> GetByIdAndIncludeChildren(
        Guid optionId, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default)
    {
        // Includes all children and prompts
        async ValueTask LoadAllChildrenAsync(
            Guid parentId,
            QueryOptions localQueryOptions,
            CancellationToken localCancellationToken)
        {
            var childOptions = await Get(executionOption => executionOption.ParentId == parentId, localQueryOptions)
                .Include(executionOption => executionOption.ChildExecutionOptions)
                .ToListAsync(localCancellationToken);
        
            foreach(var childOption in childOptions)
                await LoadAllChildrenAsync(childOption.Id, localQueryOptions, localCancellationToken);
        }
        
        var executionOption = await Get(executionOption => executionOption.Id == optionId, queryOptions)
            .Include(executionOption => executionOption.ChildExecutionOptions)
            .FirstOrDefaultAsync(cancellationToken);
        
        await LoadAllChildrenAsync(executionOption.Id, queryOptions, cancellationToken);
        
        return executionOption;
    }

    // /// <summary>
    // /// Loads all children of the given execution option
    // /// </summary>
    // /// <param name="parentOptionId"></param>
    // /// <param name="queryOptions"></param>
    // /// <param name="cancellationToken">Cancellation token</param>
    // private async ValueTask<IReadOnlyList<WorkflowExecutionOption>> LoadAllChildrenAsync(
    //     Guid parentOptionId,
    //     QueryOptions queryOptions,
    //     CancellationToken cancellationToken = default
    // )
    // {
    //     // Load children
    //     var childrenOptions = await Get(executionOption => executionOption.ParentId == parentOptionId, queryOptions)
    //         .Include(executionOption => executionOption.AnalysisPromptCategory)
    //         .ThenInclude(category => category.SelectedPrompt)
    //         .Include(executionOption => executionOption.ChildExecutionOptions)
    //         .AsSplitQuery()
    //         .ToListAsync(cancellationToken: cancellationToken);
    //     
    //     // Load grand children
    //     foreach(var childrenOption in childrenOptions)
    //     {
    //         childrenOption.AnalysisPromptCategory.SelectedPrompt!.Category = childrenOption.AnalysisPromptCategory;
    //         childrenOption.ChildExecutionOptions = await LoadAllChildrenAsync(childrenOption.Id, queryOptions, cancellationToken);
    //     }
    //     
    //     return childrenOptions;
    // }
}