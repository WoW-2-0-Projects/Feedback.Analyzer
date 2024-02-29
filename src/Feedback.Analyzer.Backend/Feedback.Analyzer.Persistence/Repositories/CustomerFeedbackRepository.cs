using System.Linq.Expressions;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a repository for managing customer feedback entities in the database.
/// </summary>
public class CustomerFeedbackRepository(AppDbContext dbContext)
    : EntityRepositoryBase<CustomerFeedback, AppDbContext>(dbContext),ICustomerFeedbackRepository
{
    public new IQueryable<CustomerFeedback> Get(Expression<Func<CustomerFeedback, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }
    public new ValueTask<CustomerFeedback?> GetByIdAsync(Guid feedbackId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(feedbackId, queryOptions, cancellationToken);
    }
    
    public new ValueTask<IList<CustomerFeedback>> GetByIdsAsync(IEnumerable<Guid> ids, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return base.GetByIdsAsync(ids, queryOptions, cancellationToken);
    }
    
    public new ValueTask<CustomerFeedback> CreateAsync(CustomerFeedback feedback, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(feedback, commandOptions, cancellationToken);
    }
    
    public new ValueTask<CustomerFeedback> UpdateAsync(CustomerFeedback feedback, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(feedback, commandOptions, cancellationToken);
    }
    
    public new ValueTask<CustomerFeedback?> DeleteByIdAsync(Guid feedbackId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(feedbackId, commandOptions, cancellationToken);
    }
    
    public new ValueTask<CustomerFeedback?> DeleteAsync(CustomerFeedback feedback, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(feedback, commandOptions, cancellationToken);
    }
}