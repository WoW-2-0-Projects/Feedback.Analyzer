using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for managing analysis workflow entities in the repository.
/// </summary>
public interface IAnalysisWorkflowsRepository
{
    /// <summary>
    /// Updates workflows in batch
    /// </summary>
    /// <param name="setPropertyCalls">Delegates to set updating property and value for it</param>
    /// <param name="batchUpdatePredicate">Filter to apply for batch update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of updated rows</returns>
    ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<AnalysisWorkflow>, SetPropertyCalls<AnalysisWorkflow>>> setPropertyCalls,
        Expression<Func<AnalysisWorkflow, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    );
}