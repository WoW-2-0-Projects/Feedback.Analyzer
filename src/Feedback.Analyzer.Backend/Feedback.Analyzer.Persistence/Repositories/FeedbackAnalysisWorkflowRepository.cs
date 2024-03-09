using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

public class FeedbackAnalysisWorkflowRepository(AppDbContext dbContext)
    : EntityRepositoryBase<FeedbackAnalysisWorkflow, AppDbContext>(dbContext), IFeedbackAnalysisWorkflowRepository
{
    public new IQueryable<FeedbackAnalysisWorkflow> Get(
        Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default,
        QueryOptions queryOptions = default
    ) =>
        base.Get(predicate, queryOptions);

    public new ValueTask<FeedbackAnalysisWorkflow?> GetByIdAsync(
        Guid workflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.GetByIdAsync(workflowId, queryOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow> CreateAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.CreateAsync(workflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow> UpdateAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.UpdateAsync(workflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow?> DeleteAsync(
        FeedbackAnalysisWorkflow workflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        base.DeleteAsync(workflow, commandOptions, cancellationToken);

    public new ValueTask<FeedbackAnalysisWorkflow?> DeleteByIdAsync(
        Guid workflowId,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default
    ) =>
        base.DeleteByIdAsync(workflowId, commandOptions, cancellationToken);
}