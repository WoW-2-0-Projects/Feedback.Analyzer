﻿using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;


/// <summary>
/// Represents a repository for managing client entities.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Retrieves a queryable collection of client entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the clients (optional).</param>
    /// <param name="asNoTracking">Indicates whether the entities should be queried without tracking changes (default is false).</param>
    /// <returns>A queryable collection of client entities.</returns>
    IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Asynchronously retrieves a client entity by its unique identifier.
    /// </summary>
    /// <param name="clientId">The unique identifier of the client.</param>
    /// <param name="asNoTracking">Indicates whether the entity should be queried without tracking changes (default is false).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the client entity, or null if not found.</returns>
    ValueTask<Client?> GetByIdAsync(Guid clientId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new client entity.
    /// </summary>
    /// <param name="client">The client entity to create.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the created client entity.</returns>
    ValueTask<Client> CreateAsync(Client client, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing client entity.
    /// </summary>
    /// <param name="client">The client entity to update.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the updated client entity.</returns>
    ValueTask<Client> UpdateAsync(Client client, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes an existing client entity.
    /// </summary>
    /// <param name="client">The client entity to delete.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the underlying data store.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted client entity, or null if not found.</returns>
    ValueTask<Client?> DeleteAsync(Client client, bool saveChanges, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously deletes a client entity by its unique identifier.
    /// </summary>
    /// <param name="clientId">The unique identifier of the client to delete.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted client entity, or null if not found.</returns>
    ValueTask<Client?> DeleteByIdAsync(Guid clientId, bool saveChanges = true, CancellationToken cancellationToken = default);
}