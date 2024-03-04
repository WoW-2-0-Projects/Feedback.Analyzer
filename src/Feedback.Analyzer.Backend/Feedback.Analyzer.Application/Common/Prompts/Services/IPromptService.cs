using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

public interface IPromptService
{
    IQueryable<AnalysisPrompt> Get(Expression<Func<AnalysisPrompt, bool>>? predicate = default, QueryOptions queryOptions = default);

    IQueryable<AnalysisPrompt> Get(PromptFilter promptFilter, QueryOptions queryOptions = default);

    ValueTask<AnalysisPrompt?> GetByIdAsync(Guid promptId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    ValueTask<AnalysisPrompt> CreateAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    ValueTask<AnalysisPrompt> UpdateAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    ValueTask<AnalysisPrompt?> DeleteAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    ValueTask<AnalysisPrompt?> DeleteByIdAsync(Guid promptId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}