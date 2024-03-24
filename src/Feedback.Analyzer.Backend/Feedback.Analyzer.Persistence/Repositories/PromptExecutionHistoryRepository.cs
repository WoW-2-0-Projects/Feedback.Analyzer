using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository for managing prompt execution history records.
/// </summary>
public class PromptExecutionHistoryRepository(AppDbContext dbContext, ICacheBroker cacheBroker) : 
    EntityRepositoryBase<PromptExecutionHistory, AppDbContext>(dbContext, cacheBroker), IPromptExecutionHistoryRepository
{
    public new IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate = default, QueryOptions queryOptions = default) 
        => base.Get(predicate, queryOptions);

    public new ValueTask<PromptExecutionHistory> CreateAsync(PromptExecutionHistory history, CommandOptions commandOptions = default, CancellationToken cancellationToken = default) 
        => base.CreateAsync(history, commandOptions, cancellationToken);
}