using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for managing customer feedback entities.
/// </summary>
public interface ICustomerFeedbackRepository
{
    /// <summary>
    /// Retrieves a queryable collection of customer feedback entities based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">An optional predicate to filter the customer feedback entities.</param>
    /// <param name="queryOptions">Query options for sorting, pagination, etc.</param>
    /// <returns>A queryable collection of customer feedback entities.</returns>
    IQueryable<CustomerFeedback> Get(Expression<Func<CustomerFeedback, bool>>? predicate = default, QueryOptions queryOptions = default);
   
    /// <summary>
    /// Retrieves a customer feedback entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="customerFeedbackId">The unique identifier of the customer feedback entity to retrieve.</param>
    /// <param name="queryOptions">Query options for sorting, pagination, etc.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation that returns the customer feedback entity.</returns>
    ValueTask<CustomerFeedback?> GetByIdAsync(Guid customerFeedbackId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);


    /// <summary>
    /// Creates a new customer feedback entity asynchronously.
    /// </summary>
    /// <param name="customerFeedback">The customer feedback entity to create.</param>
    /// <param name="commandOptions">Command options for the create operation.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation that returns the created customer feedback entity.</returns>
    ValueTask<CustomerFeedback> CreateAsync(CustomerFeedback customerFeedback, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates a customer feedback entity asynchronously.
    /// </summary>
    /// <param name="customerFeedback">The customer feedback entity to update.</param>
    /// <param name="commandOptions">Command options for the update operation.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation that returns the updated customer feedback entity.</returns>
    ValueTask<CustomerFeedback?> UpdateAsync(CustomerFeedback customerFeedback, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes a customer feedback entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="customerFeedbackId">The unique identifier of the customer feedback entity to delete.</param>
    /// <param name="commandOptions">Command options for the delete operation.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask<CustomerFeedback?> DeleteByIdAsync(Guid customerFeedbackId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}