using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.FeedbackWorkflows.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.Services;

public class FeedbackWorkflowService(
    IFeedbackWorkflowRepository feedbackWorkflowRepository, 
    FeedbackWorkflowValidator feedbackWorkflowValidator)
    : IFeedbackWorkflowService
{
    public IQueryable<FeedbackWorkflow> Get(Expression<Func<FeedbackWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default)
        => feedbackWorkflowRepository.Get(predicate, queryOptions);

    public IQueryable<FeedbackWorkflow> Get(
        FeedbackWorkflowFilter feedbackWorkflowFilter,
        QueryOptions queryOptions = default)
        => feedbackWorkflowRepository.Get().ApplyPagination(feedbackWorkflowFilter);

    public ValueTask<FeedbackWorkflow> GetByIdAsync(
        Guid feedbackWorkflowId,
        QueryOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => feedbackWorkflowRepository.GetByIdAsync(feedbackWorkflowId, commandOptions, cancellationToken);

    public ValueTask<FeedbackWorkflow> CreateAsync(
        FeedbackWorkflow feedbackWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = feedbackWorkflowValidator
            .Validate(feedbackWorkflow,
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return feedbackWorkflowRepository.CreateAsync(feedbackWorkflow, commandOptions, cancellationToken);
    }

    public async ValueTask<FeedbackWorkflow> UpdateAsync(
        FeedbackWorkflow feedbackWorkflow, 
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        
        var validationResult = feedbackWorkflowValidator
            .Validate(feedbackWorkflow,
                options => options.IncludeRuleSets(EntityEvent.OnUpdate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var foundFeedWorkflow = await GetByIdAsync(feedbackWorkflow.Id, cancellationToken: cancellationToken) ??
                                throw new InvalidOperationException();

        foundFeedWorkflow.Name = feedbackWorkflow.Name;

        return await feedbackWorkflowRepository.UpdateAsync(foundFeedWorkflow, commandOptions, cancellationToken);
    }

    public ValueTask<FeedbackWorkflow> DeleteByIdAsync(
        Guid feedbackWorkflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => feedbackWorkflowRepository.DeleteByIdAsync(feedbackWorkflowId, commandOptions, cancellationToken);
}