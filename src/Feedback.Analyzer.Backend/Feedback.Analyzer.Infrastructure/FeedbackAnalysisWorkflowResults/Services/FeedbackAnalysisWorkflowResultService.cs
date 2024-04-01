using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.Services;

/// <summary>
/// Interface for managing FeedbackAnalysisWorkflowResult entities.
/// </summary>
public class FeedbackAnalysisWorkflowResultService(
    IValidator<FeedbackAnalysisWorkflowResult> workflowResultValidator,
    IFeedbackAnalysisWorkflowResultRepository feedbackAnalysisWorkflowResultRepository
) : IFeedbackAnalysisWorkflowResultService
{
    public IQueryable<FeedbackAnalysisWorkflowResult> Get(
        Expression<Func<FeedbackAnalysisWorkflowResult, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        feedbackAnalysisWorkflowResultRepository.Get(predicate, queryOptions);

    public ValueTask<FeedbackAnalysisWorkflowResult?> GetByIdAsync(
        Guid workflowResultId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowResultRepository.GetByIdAsync(workflowResultId, queryOptions, cancellationToken);

    public async ValueTask<FeedbackAnalysisWorkflowResult> CreateAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await workflowResultValidator.ValidateAsync(workflowResult, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return await feedbackAnalysisWorkflowResultRepository.CreateAsync(workflowResult, commandOptions, cancellationToken);
    }

    public async ValueTask<FeedbackAnalysisWorkflowResult> UpdateAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await workflowResultValidator.ValidateAsync(workflowResult, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        return await feedbackAnalysisWorkflowResultRepository.UpdateAsync(workflowResult, commandOptions, cancellationToken);
    }

    public ValueTask<FeedbackAnalysisWorkflowResult?> DeleteByIdAsync(
        Guid workflowResultId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowResultRepository.DeleteByIdAsync(workflowResultId, commandOptions, cancellationToken);
}