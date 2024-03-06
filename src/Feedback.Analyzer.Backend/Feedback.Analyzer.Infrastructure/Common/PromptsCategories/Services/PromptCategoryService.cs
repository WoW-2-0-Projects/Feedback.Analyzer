using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Application.Common.PromptCategory.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.Services;

/// <summary>
/// Represents a service that provides operations for prompt categories.
/// </summary>
public class PromptCategoryService(IPromptCategoryRepository promptCategoryRepository)
    : IPromptCategoryService
{
    public IQueryable<AnalysisPromptCategory> Get(Expression<Func<AnalysisPromptCategory, bool>>? predicate = default,
                                                  QueryOptions queryOptions = default)

        => promptCategoryRepository.Get(predicate, queryOptions);
    
    public IQueryable<AnalysisPromptCategory> Get(PromptCategoryFilter promptCategoryFilter, QueryOptions queryOptions = default)
    
        => promptCategoryRepository.Get(queryOptions: queryOptions).ApplyPagination(promptCategoryFilter);
    
}