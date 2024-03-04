using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Prompts;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Services;

public class PromptService(IPromptRepository promptRepository, IValidator<AnalysisPrompt> promptValidator, IPromptRepository repo) : IPromptService
{
    public IQueryable<AnalysisPrompt> Get(Expression<Func<AnalysisPrompt, bool>>? predicate = default, QueryOptions queryOptions = default) =>
        promptRepository.Get(predicate, queryOptions);

    public IQueryable<AnalysisPrompt> Get(PromptFilter promptFilter, QueryOptions queryOptions = default)
        => promptRepository.Get(queryOptions: queryOptions).ApplyPagination(promptFilter);

    public ValueTask<AnalysisPrompt?> GetByIdAsync(Guid promptId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        => promptRepository.GetByIdAsync(promptId, queryOptions, cancellationToken);
    
    public async ValueTask<AnalysisPrompt> CreateAsync(
        AnalysisPrompt prompt,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var test = promptRepository;
        
        var validationResult = promptValidator.Validate(prompt,
            options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Get latest prompt version
        var latestPromptMajorVersion = await promptRepository
            .Get()
            .OrderByDescending(p => p.Version)
            .Where(existingPrompt => existingPrompt.CategoryId == prompt.CategoryId)
            .Select(existingPrompt => existingPrompt.Revision)
            .FirstOrDefaultAsync(_ => true, cancellationToken);
        
        // Increment version
        prompt.Version = latestPromptMajorVersion + 1;

        return await promptRepository.CreateAsync(prompt, commandOptions, cancellationToken);
    }

    public async ValueTask<AnalysisPrompt> UpdateAsync(
        AnalysisPrompt prompt,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = promptValidator.Validate(prompt,
            options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        // Get latest prompt revision
        var latestMinorVersion = await promptRepository
            .Get()
            .OrderByDescending(p => p.Revision)
            .Where(existingPrompt => existingPrompt.CategoryId == prompt.CategoryId)
            .Select(existingPrompt => existingPrompt.Revision)
            .FirstOrDefaultAsync(_ => true, cancellationToken);
        
        // Clone the prompt
        var clonedPrompt = prompt.Clone();
        
        // Increment minor version
        clonedPrompt.Revision = latestMinorVersion + 1;

        return await promptRepository.CreateAsync(clonedPrompt, commandOptions, cancellationToken);
    }

    public ValueTask<AnalysisPrompt?> DeleteAsync(
        AnalysisPrompt prompt,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    => promptRepository.DeleteAsync(prompt, commandOptions, cancellationToken);

    public ValueTask<AnalysisPrompt?> DeleteByIdAsync(
        Guid promptId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    => promptRepository.DeleteByIdAsync(promptId, commandOptions, cancellationToken);
}