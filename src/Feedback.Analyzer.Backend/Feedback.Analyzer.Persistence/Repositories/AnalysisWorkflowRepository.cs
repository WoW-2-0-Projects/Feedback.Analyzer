using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a repository for managing analysis workflows in the database.
/// </summary>
public class AnalysisWorkflowRepository(AppDbContext appDbContext) : EntityRepositoryBase<AnalysisWorkflow, AppDbContext>(appDbContext), IAnalysisWorkflowRepository
{
    public new IQueryable<AnalysisWorkflow> Get(Expression<Func<AnalysisWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default)
        => base.Get(predicate, queryOptions);

    public new ValueTask<AnalysisWorkflow?> GetByIdAsync(Guid analysisWorkflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(analysisWorkflowId, queryOptions, cancellationToken);

    public new ValueTask<bool> CheckByIdAsync(Guid workflowId, CancellationToken cancellationToken = default)
        => base.CheckByIdAsync(workflowId, cancellationToken);

    public new ValueTask<AnalysisWorkflow> CreateAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.CreateAsync(analysisWorkflow, commandOptions, cancellationToken);

    public new ValueTask<AnalysisWorkflow> UpdateAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.UpdateAsync(analysisWorkflow, commandOptions, cancellationToken);

    public new ValueTask<AnalysisWorkflow?> DeleteAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.DeleteAsync(analysisWorkflow, commandOptions, cancellationToken);

    public new ValueTask<AnalysisWorkflow?> DeleteByIdAsync(Guid analysisWorkflowId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(analysisWorkflowId, commandOptions, cancellationToken);
}