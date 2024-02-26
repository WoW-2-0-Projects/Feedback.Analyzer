using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines the contract for interacting with organization data in the system.
/// </summary>
public interface IOrganizationRepository
{
    /// <summary>
    /// Retrieves a queryable collection of organizations based on a provided predicate.
    /// </summary>
    /// <param name="predicate">An optional expression to filter the results.</param>
    /// <param name="asNoTracking">Whether to disable entity change tracking for performance optimization.</param>
    /// <returns>An IQueryable of Organization objects.</returns>
    IQueryable<Organization> Get(Expression<Func<Organization, bool>>? predicate, bool asNoTracking = false);
    
    
    /// <summary>
    /// Retrieves an Organization by its unique identifier (ID).
    /// </summary>
    /// <param name="organizationId">The unique ID of the Organization.</param>
    /// <param name="asNoTracking">If true, disables change tracking for the returned entity, potentially improving performance.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The requested Organization if found, otherwise null.</returns>
    ValueTask<Organization?> GetByIdAsync(Guid organizationId, bool asNoTracking = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves a list of Organizations based on a collection of their unique identifiers (IDs).
    /// </summary>
    /// <param name="ids">A collection of Organization IDs.</param>
    /// <param name="asNoTracking">If true, disables change tracking for the returned entities, potentially improving performance.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A list of Organizations matching the provided IDs.  If an ID doesn't correspond to an existing Organization, it is omitted in the result.</returns> 
    ValueTask<IList<Organization>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new Organization record.
    /// </summary>
    /// <param name="organization">The Organization object to be created.</param>
    /// <param name="saveChanges">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The newly created Organization object.</returns>
    /// <exception cref="ArgumentException">Thrown if the provided 'organization' has a default (empty) ID or if an organization with the same ID already exists.</exception>
    ValueTask<Organization> CreateAsync(Organization organization, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an existing Organization record.
    /// </summary>
    /// <param name="organization">The Organization object containing modified data.</param>
    /// <param name="saveChanges">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The updated Organization object.</returns> 
    /// <exception cref="ArgumentNullException">Thrown if the provided 'organization' is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the Organization cannot be found for the provided ID.</exception>
    ValueTask<Organization> UpdateAsync(Organization organization, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an existing Organization record.
    /// </summary>
    /// <param name="organization">The Organization object to be deleted.</param>
    /// <param name="saveChanges">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Organization object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided 'organization' is null.</exception> 
    ValueTask<Organization?> DeleteAsync(Organization organization, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an existing Organization record based on its unique identifier (ID).
    /// </summary>
    /// <param name="organizationId">The ID of the Organization to be deleted.</param>
    /// <param name="saveChanges">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Organization object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    ValueTask<Organization?> DeleteByIdAsync(Guid organizationId, bool saveChanges = true, CancellationToken cancellationToken = default);
}