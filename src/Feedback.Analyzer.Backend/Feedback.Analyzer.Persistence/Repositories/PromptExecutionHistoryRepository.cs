using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.Repositories;

public class PromptExecutionHistoryRepository(AppDbContext dbContext) : 
    EntityRepositoryBase<PromptExecutionHistory, AppDbContext>(dbContext), IPromptExecutionHistoryRepository
{
    public new IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new async ValueTask<PromptExecutionHistory> CreateAsync(PromptExecutionHistory history, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        var prompt = history.Prompt;
        history.Prompt = null;
        
        // DbContext.Entry(history.Prompt).State = EntityState.Detached;
        
        var createdEvent = await base.CreateAsync(history, commandOptions, cancellationToken);
        
        // DbContext.Entry(history.Prompt).State = EntityState.Unchanged;
        history.Prompt = prompt;
        
        return createdEvent;
    }
}