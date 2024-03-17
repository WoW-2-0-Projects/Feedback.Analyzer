using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.Services;

/// <summary>
/// Interface for managing FeedbackAnalysisWorkflowResult entities.
/// </summary>
public class FeedbackAnalysisWorkflowResultService(IFeedbackAnalysisWorkflowResultRepository feedbackAnalysisWorkflowResultRepository)
    : IFeedbackAnalysisWorkflowResultService
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

    public ValueTask<FeedbackAnalysisWorkflowResult> CreateAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowResultRepository.CreateAsync(workflowResult, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflowResult> UpdateAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowResultRepository.UpdateAsync(workflowResult, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflowResult?> DeleteByIdAsync(
        Guid workflowResultId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowResultRepository.DeleteByIdAsync(workflowResultId, commandOptions, cancellationToken);
}