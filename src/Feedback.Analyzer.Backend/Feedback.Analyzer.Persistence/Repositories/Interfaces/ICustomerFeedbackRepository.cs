using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines a repository interface for managing customer feedback entities.
/// </summary>
public interface ICustomerFeedbackRepository
{
    /// <summary>
    /// Retrieves a queryable collection of customer feedback entities based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">A predicate to filter the feedback entities.</param>
    /// <param name="queryOptions">Additional query options.</param>
    /// <returns>An IQueryable collection of customer feedback entities.</returns>
    IQueryable<CustomerFeedback> Get(Expression<Func<CustomerFeedback, bool>>? predicate = default,
                                      QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a customer feedback entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="feedbackId">The unique identifier of the customer feedback entity.</param>
    /// <param name="queryOptions">Additional query options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns the customer feedback entity, or null if not found.</returns>
    ValueTask<CustomerFeedback?> GetByIdAsync(Guid feedbackId,
                                               QueryOptions queryOptions = default,
                                               CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves customer feedback entities by their unique identifiers asynchronously.
    /// </summary>
    /// <param name="ids">The unique identifiers of the customer feedback entities.</param>
    /// <param name="queryOptions">Additional query options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of customer feedback entities.</returns>
    ValueTask<IList<CustomerFeedback>> GetByIdsAsync(IEnumerable<Guid> ids,
                                                      QueryOptions queryOptions = default,
                                                      CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new customer feedback entity asynchronously.
    /// </summary>
    /// <param name="feedback">The customer feedback entity to create.</param>
    /// <param name="commandOptions">Additional command options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns the created customer feedback entity.</returns>
    ValueTask<CustomerFeedback> CreateAsync(CustomerFeedback feedback,
                                             CommandOptions commandOptions = default,
                                             CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing customer feedback entity asynchronously.
    /// </summary>
    /// <param name="feedback">The customer feedback entity to update.</param>
    /// <param name="commandOptions">Additional command options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns the updated customer feedback entity.</returns>
    ValueTask<CustomerFeedback> UpdateAsync(CustomerFeedback feedback,
                                             CommandOptions commandOptions = default,
                                             CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer feedback entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="feedbackId">The unique identifier of the customer feedback entity to delete.</param>
    /// <param name="commandOptions">Additional command options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns the deleted customer feedback entity, or null if not found.</returns>
    ValueTask<CustomerFeedback?> DeleteByIdAsync(Guid feedbackId,
                                                  CommandOptions commandOptions = default,
                                                  CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer feedback entity asynchronously.
    /// </summary>
    /// <param name="feedback">The customer feedback entity to delete.</param>
    /// <param name="commandOptions">Additional command options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns the deleted customer feedback entity, or null if not found.</returns>
    ValueTask<CustomerFeedback?> DeleteAsync(CustomerFeedback feedback,
                                              CommandOptions commandOptions = default,
                                              CancellationToken cancellationToken = default);
}

