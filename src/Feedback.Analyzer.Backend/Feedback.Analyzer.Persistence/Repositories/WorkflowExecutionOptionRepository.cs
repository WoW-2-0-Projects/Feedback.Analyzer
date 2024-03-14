using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Extensions;
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
    public async ValueTask<WorkflowExecutionOption?> GetByIdAndIncludeAllGrandChildrenAsync(
        Guid optionId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var executionOption = await Get(executionOption => executionOption.Id == optionId, queryOptions)
                                    .Include(executionOption => executionOption.Workflow)
                                    .Include(executionOption => executionOption.AnalysisPromptCategory)
                                    .ThenInclude(category => category.SelectedPrompt)
                                    .Include(executionOption => executionOption.ChildExecutionOptions)
                                    .AsSplitQuery()
                                    .ApplyTrackingMode(queryOptions.TrackingMode)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (executionOption is not null)
        {
            executionOption.AnalysisPromptCategory.SelectedPrompt!.Category = executionOption.AnalysisPromptCategory;
            executionOption.ChildExecutionOptions = await LoadAllChildrenAsync(executionOption.Id, queryOptions, cancellationToken);
        }
        
        return executionOption;
    }
    /// <summary>
    /// Loads all children of the given execution option
    /// </summary>
    /// <param name="parentOptionId"></param>
    /// <param name="queryOptions"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    private async ValueTask<IReadOnlyList<WorkflowExecutionOption>> LoadAllChildrenAsync(
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
                async childrenOption =>
                {
                    childrenOption.AnalysisPromptCategory.SelectedPrompt!.Category = childrenOption.AnalysisPromptCategory;
                    childrenOption.ChildExecutionOptions = await LoadAllChildrenAsync(childrenOption.Id, queryOptions, cancellationToken);
                }
            )
        );
        return childrenOptions;
    }
}