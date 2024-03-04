using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsHistory.Services;

public class PromptExecutionHistoryService(
    IPromptExecutionHistoryRepository promptExecutionHistoryRepository,
    IValidator<PromptExecutionHistory> validator
    ) : IPromptsExecutionHistoryService
{
    public IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate, QueryOptions queryOptions)
    {
        return promptExecutionHistoryRepository.Get(predicate, queryOptions);
    }

    public async ValueTask<IList<PromptExecutionHistory>> GetByPromptIdAsync(Guid promptId, QueryOptions queryOptions, CancellationToken cancellationToken)
    {
        return await Get(prompt => prompt.PromptId == promptId, queryOptions).ToListAsync(cancellationToken);
    }

    public async ValueTask<PromptExecutionHistory> CreateAsync(PromptExecutionHistory promptExecutionHistory, CommandOptions commandOptions, CancellationToken cancellationToken)
    {
        return await promptExecutionHistoryRepository.CreateAsync(promptExecutionHistory, commandOptions, cancellationToken);
    }
}