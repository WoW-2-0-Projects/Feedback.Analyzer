using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

public class PromptExecutionHistoryRepository(AppDbContext dbContext) : 
    EntityRepositoryBase<PromptExecutionHistory, AppDbContext>(dbContext), IPromptExecutionHistoryRepository
{
    public new IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<PromptExecutionHistory> CreateAsync(PromptExecutionHistory prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(prompt, commandOptions, cancellationToken);
    }
}