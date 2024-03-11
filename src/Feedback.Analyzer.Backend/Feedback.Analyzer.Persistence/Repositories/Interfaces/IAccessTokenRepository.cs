﻿using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for accessing and managing AccessToken entities in a data store.
/// </summary>
public interface IAccessTokenRepository
{
    /// <summary>
    /// Asynchronously creates a new AccessToken entity.
    /// </summary>
    /// <param name="accessToken">The AccessToken entity to be created.</param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none).</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the created AccessToken.</returns>
    ValueTask<AccessToken> CreateAsync(AccessToken accessToken, CommandOptions commandOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves an AccessToken entity by its unique identifier.
    /// </summary>
    /// <param name="accessTokenId">The unique identifier of the AccessToken entity to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none).</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the retrieved AccessToken, or null if not found.</returns>
    ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing AccessToken entity.
    /// </summary>
    /// <param name="accessToken">The AccessToken entity to be updated.</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none).</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the updated AccessToken.</returns>
    ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes access token by Id
    /// </summary>
    /// <param name="accessTokenId">Id of access token to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    ValueTask<AccessToken?> DeleteByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default);
}