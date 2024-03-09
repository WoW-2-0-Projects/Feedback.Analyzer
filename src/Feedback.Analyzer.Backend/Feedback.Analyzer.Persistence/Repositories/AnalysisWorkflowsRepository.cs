using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Feedback.Analyzer.Persistence.Repositories;

public class AnalysisWorkflowsRepository(AppDbContext dbContext)
    : EntityRepositoryBase<AnalysisWorkflow, AppDbContext>(dbContext), IAnalysisWorkflowsRepository
{
    public new ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<AnalysisWorkflow>, SetPropertyCalls<AnalysisWorkflow>>> setPropertyCalls,
        Expression<Func<AnalysisWorkflow, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    ) =>
        base.UpdateBatchAsync(setPropertyCalls, batchUpdatePredicate, cancellationToken);
}