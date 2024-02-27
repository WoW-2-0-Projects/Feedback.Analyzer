using System.Linq.Expressions;
using Feedback.Analyzer.Application.Clients.Model;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Services;

/// <summary>
/// Represents a service for managing client entities.
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Retrieves a queryable collection of client entities based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">A predicate to filter the clients (optional).</param>
    /// <param name="queryOptions">Options for customizing the query (optional).</param>
    /// <returns>A queryable collection of client entities.</returns>
    IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a queryable collection of client entities based on the specified client filter and query options.
    /// </summary>
    /// <param name="clientFilter">A filter to apply when retrieving clients.</param>
    /// <param name="queryOptions">Options for customizing the query (optional).</param>
    /// <returns>A queryable collection of client entities.</returns>
    IQueryable<Client> Get(ClientFilter clientFilter, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Asynchronously retrieves a client entity by its unique identifier and query options.
    /// </summary>
    /// <param name="clientId">The unique identifier of the client.</param>
    /// <param name="queryOptions">Options for customizing the query (optional).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the client entity, or null if not found.</returns>
    ValueTask<Client?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing client entity with the specified options.
    /// </summary>
    /// <param name="client">The client entity to update.</param>
    /// <param name="commandOptions">Options for customizing the command (optional).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the updated client entity.</returns>
    ValueTask<Client?> UpdateAsync(Client client,  CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously deletes a client entity by its unique identifier with the specified options.
    /// </summary>
    /// <param name="clientId">The unique identifier of the client to delete.</param>
    /// <param name="commandOptions">Options for customizing the command (optional).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted client entity, or null if not found.</returns>
    ValueTask<Client?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

}