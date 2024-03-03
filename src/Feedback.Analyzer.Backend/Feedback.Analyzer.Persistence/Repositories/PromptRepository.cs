using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Prompts;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing client entities.
/// </summary>
public class PromptRepository(AppDbContext dbContext) : EntityRepositoryBase<AnalysisPrompt, AppDbContext>(dbContext), IPromptRepository
{
    public new IQueryable<AnalysisPrompt> Get(Expression<Func<AnalysisPrompt, bool>>? predicate = default, QueryOptions queryOptions = default)
     => base.Get(predicate, queryOptions);

    public new ValueTask<AnalysisPrompt?> GetByIdAsync(Guid promptId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
     => base.GetByIdAsync(promptId, queryOptions, cancellationToken);

    public new ValueTask<AnalysisPrompt> UpdateAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
     => base.UpdateAsync(prompt, commandOptions, cancellationToken);

    public new ValueTask<AnalysisPrompt?> DeleteByIdAsync(Guid promptId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    => base.DeleteByIdAsync(promptId, commandOptions, cancellationToken);
}