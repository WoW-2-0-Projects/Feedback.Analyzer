using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Identity.Services;

/// <summary>
/// Represents a service for managing user accounts.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="emailAddress">The email address of the user to retrieve.</param>
    /// <param name="asNoTracking">Indicates whether to disable change tracking. Default is false.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>A <see cref="ValueTask"/> containing the retrieved user or null if not found.</returns>
    ValueTask<Client?> GetUserByEmailAddressAsync(
        string emailAddress,
        QueryOptions options = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user object to be created.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>True if the user is created successfully; otherwise, false.</returns>
    ValueTask<Client> CreateClientAsync(Client user, CancellationToken cancellationToken = default);
    
}
