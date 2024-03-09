using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.Workflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.Services;

public class FeedbackAnalysisWorkflowService(IFeedbackAnalysisWorkflowRepository feedbackAnalysisWorkflowRepository)
    : IFeedbackAnalysisWorkflowService
{
    public IQueryable<FeedbackAnalysisWorkflow> Get(
        Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        feedbackAnalysisWorkflowRepository.Get(predicate, queryOptions);

    public ValueTask<FeedbackAnalysisWorkflow?> GetByIdAsync(
        Guid workflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.GetByIdAsync(workflowId, queryOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflow> CreateAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.CreateAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflow> UpdateAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.UpdateAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflow?> DeleteAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.DeleteAsync(workflow, commandOptions, cancellationToken);

    public ValueTask<FeedbackAnalysisWorkflow?> DeleteByIdAsync(
        Guid workflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        feedbackAnalysisWorkflowRepository.DeleteByIdAsync(workflowId, commandOptions, cancellationToken);
}