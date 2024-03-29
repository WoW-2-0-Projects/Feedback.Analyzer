using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Provides foundation service for feedback analysis workflow
/// </summary>
public class FeedbackAnalysisWorkflowService(
    IFeedbackAnalysisWorkflowRepository feedbackAnalysisWorkflowRepository,
     FeedbackAnalysisWorkflowValidator analysisWorkflowValidator
    ) : IFeedbackAnalysisWorkflowService
{
    public IQueryable<FeedbackAnalysisWorkflow> Get(
        Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default)
        => feedbackAnalysisWorkflowRepository.Get(predicate, queryOptions);

    public IQueryable<FeedbackAnalysisWorkflow> Get(
        FeedbackAnalysisWorkflowFilter feedbackAnalysisWorkflowFilter,
        QueryOptions queryOptions = default
    )
    {
        var workflowsQuery = feedbackAnalysisWorkflowRepository.Get().ApplyPagination(feedbackAnalysisWorkflowFilter);

        if (feedbackAnalysisWorkflowFilter.ClientId.HasValue)
            workflowsQuery = workflowsQuery
                .Include(workflow => workflow.AnalysisWorkflow)
                .Include(workflow => workflow.Product)
                    .ThenInclude(product => product.Organization)
                .Where(workflow => workflow.Product.Organization.ClientId == feedbackAnalysisWorkflowFilter.ClientId.Value 
                                   || workflow.AnalysisWorkflow.Type == WorkflowType.Template);

        return workflowsQuery;
    }
    
    public ValueTask<FeedbackAnalysisWorkflow?> GetByIdAsync(
        Guid feedbackAnalysisWorkflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
        => feedbackAnalysisWorkflowRepository.GetByIdAsync(feedbackAnalysisWorkflowId, queryOptions, cancellationToken);
    
    public ValueTask<FeedbackAnalysisWorkflow> CreateAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = analysisWorkflowValidator
            .ValidateAsync(feedbackAnalysisWorkflow,
                options =>
                    options.IncludeRuleSets(EntityEvent.OnCreate.ToString()), cancellation: cancellationToken);

        if (!validationResult.Result.IsValid)
            throw new ValidationException(validationResult.Result.Errors);

        return feedbackAnalysisWorkflowRepository.CreateAsync(feedbackAnalysisWorkflow, commandOptions,
            cancellationToken);
    }
    
    public ValueTask<FeedbackAnalysisWorkflow> DeleteAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => feedbackAnalysisWorkflowRepository.DeleteAsync(feedbackAnalysisWorkflow, commandOptions, cancellationToken);
    
    public ValueTask<FeedbackAnalysisWorkflow> DeleteByIdAsync(
        Guid feedbackAnalysisWorkflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => feedbackAnalysisWorkflowRepository.DeleteByIdAsync(feedbackAnalysisWorkflowId, commandOptions,
            cancellationToken);
}