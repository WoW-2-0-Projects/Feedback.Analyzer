using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflows.Validators;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflows.Services;

/// <summary>
/// Represents a service for managing analysis workflows.
/// </summary>
public class AnalysisWorkflowService(
    IAnalysisWorkflowRepository analysisWorkflowRepository,
    AnalysisWorkflowValidator analysisWorkflowValidator) : IAnalysisWorkflowService
{
    public IQueryable<AnalysisWorkflow> Get(
        Expression<Func<AnalysisWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default)
        => analysisWorkflowRepository.Get(predicate, queryOptions);

    public ValueTask<AnalysisWorkflow?> GetByIdAsync(
        Guid analysisWorkflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
        => analysisWorkflowRepository.GetByIdAsync(analysisWorkflowId, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(Guid analysisId, CancellationToken cancellationToken = default)
        => analysisWorkflowRepository.CheckByIdAsync(analysisId, cancellationToken);

    public ValueTask<AnalysisWorkflow> CreateAsync(
        AnalysisWorkflow analysisWorkflow, 
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = analysisWorkflowValidator
            .Validate(analysisWorkflow, 
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return analysisWorkflowRepository.CreateAsync(analysisWorkflow, commandOptions, cancellationToken);
    }

    public async ValueTask<AnalysisWorkflow> UpdateAsync(
        AnalysisWorkflow analysisWorkflow, 
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await analysisWorkflowValidator
            .ValidateAsync(analysisWorkflow,
                options => options.IncludeRuleSets(EntityEvent.OnUpdate.ToString()), cancellation: cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ToString());

        var foundAnalysisWorkflow = (await GetByIdAsync(analysisWorkflow.Id, cancellationToken: cancellationToken))!;

        foundAnalysisWorkflow.Name = analysisWorkflow.Name;

        return await analysisWorkflowRepository.UpdateAsync(foundAnalysisWorkflow, commandOptions, cancellationToken);
    }

    public ValueTask<AnalysisWorkflow?> DeleteAsync(
        AnalysisWorkflow analysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => analysisWorkflowRepository.DeleteAsync(analysisWorkflow, commandOptions, cancellationToken);

    public ValueTask<AnalysisWorkflow?> DeleteByIdAsync(
        Guid analysisWorkflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => analysisWorkflowRepository.DeleteByIdAsync(analysisWorkflowId, commandOptions, cancellationToken);
}