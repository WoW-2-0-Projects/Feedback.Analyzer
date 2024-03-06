using System.Collections.Immutable;
using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.PromptCategories.Models;
using Feedback.Analyzer.Application.Common.PromptCategories.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Common.PromptCategories.Services;

public class PromptCategoryService(IPromptCategoryRepository promptCategoryRepository, IValidator<AnalysisPrompt> promptValidator)
    : IPromptCategoryService
{
    public IValidator<AnalysisPrompt> PromptValidator { get; } = promptValidator;

    public IQueryable<AnalysisPromptCategory> Get(
        Expression<Func<AnalysisPromptCategory, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        promptCategoryRepository.Get(predicate, queryOptions);

    public IQueryable<AnalysisPromptCategory> Get(PromptCategoryFilter promptCategoryFilter, QueryOptions queryOptions = default) =>
        promptCategoryRepository.Get(queryOptions: queryOptions).ApplyPagination(promptCategoryFilter);

    public async ValueTask<bool> UpdateSelectedPromptIdAsync(Guid promptCategoryId, Guid promptId, CancellationToken cancellationToken = default)
    {
        // return await promptCategoryRepository
        //     .UpdateBatchAsync(
        //     ImmutableList.Create<(Func<AnalysisPromptCategory, object>, Func<AnalysisPromptCategory, object>)>(
        //         (category => category.SelectedPromptId!, _ => promptId)
        //     ),
        //     category => category.Id == promptCategoryId,
        //     cancellationToken
        // ) == 1;

        return await promptCategoryRepository.UpdateBatchAsync(
            categorySetCall => categorySetCall.SetProperty(category => category.SelectedPromptId, promptId),
            category => category.Id == promptCategoryId,
            cancellationToken
        ) == 1;
    }
}