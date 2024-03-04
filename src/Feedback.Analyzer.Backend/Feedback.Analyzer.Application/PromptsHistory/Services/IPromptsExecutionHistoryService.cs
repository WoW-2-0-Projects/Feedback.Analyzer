using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.PromptsHistory.Services;

public interface IPromptsExecutionHistoryService
{
    IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate,
        QueryOptions queryOptions);

    ValueTask<IList<PromptExecutionHistory>> GetByPromptIdAsync(
        Guid promptId,
        QueryOptions queryOptions,
        CancellationToken cancellationToken
        );

    ValueTask<PromptExecutionHistory> CreateAsync(
        PromptExecutionHistory promptExecutionHistory, 
        CommandOptions commandOptions,
        CancellationToken cancellationToken
        );
}