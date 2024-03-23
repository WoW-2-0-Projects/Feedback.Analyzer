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
        async ValueTask<IReadOnlyList<WorkflowExecutionOption>> LoadAllChildrenAsync(
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
                childOption.ChildExecutionOptions = await LoadAllChildrenAsync(childOption.Id, localQueryOptions, localCancellationToken);
            }

            return childOptions;
        }

        // Main method logic
        var executionOption = await Get(executionOption => executionOption.Id == optionId, queryOptions)
            .Include(executionOption => executionOption.AnalysisPromptCategory)
            .ThenInclude(category => category.SelectedPrompt)
            .Include(executionOption => executionOption.ChildExecutionOptions)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        if (executionOption is not null)
            executionOption.ChildExecutionOptions = await LoadAllChildrenAsync(executionOption.Id, queryOptions, cancellationToken);
    
        return executionOption;
    }

    public async ValueTask<WorkflowExecutionOption> GetByIdAndIncludeChildren(
        Guid optionId, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default)
    {
        // Includes all children and prompts
        async ValueTask<IReadOnlyList<WorkflowExecutionOption>> LoadAllChildrenAsync(
            Guid parentId,
            QueryOptions localQueryOptions,
            CancellationToken localCancellationToken)
        {
            var childOptions = await Get(executionOption => executionOption.ParentId == parentId, localQueryOptions)
                .Include(executionOption => executionOption.ChildExecutionOptions)
                .ToListAsync(localCancellationToken);
        
            foreach(var childOption in childOptions)
                childOption.ChildExecutionOptions = await LoadAllChildrenAsync(childOption.Id, localQueryOptions, localCancellationToken);

            return childOptions;
        }
        
        var executionOption = await Get(executionOption => executionOption.Id == optionId, queryOptions)
            .Include(executionOption => executionOption.ChildExecutionOptions)
            .FirstOrDefaultAsync(cancellationToken);
        
        executionOption.ChildExecutionOptions = await LoadAllChildrenAsync(executionOption.Id, queryOptions, cancellationToken);
        
        return executionOption;
    }
}