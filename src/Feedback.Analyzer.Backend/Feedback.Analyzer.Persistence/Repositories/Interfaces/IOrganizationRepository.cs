using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
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
    /// <param name="queryOptions">Whether to disable entity change tracking for performance optimization.</param>
    /// <returns>An IQueryable of Organization objects.</returns>
    IQueryable<Organization> Get(Expression<Func<Organization, bool>>? predicate, QueryOptions queryOptions = default);
    
    
    /// <summary>
    /// Retrieves an Organization by its unique identifier (ID).
    /// </summary>
    /// <param name="organizationId">The unique ID of the Organization.</param>
    /// <param name="queryOptions">If true, disables change tracking for the returned entity, potentially improving performance.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The requested Organization if found, otherwise null.</returns>
    ValueTask<Organization?> GetByIdAsync(Guid organizationId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves a list of Organizations based on a collection of their unique identifiers (IDs).
    /// </summary>
    /// <param name="ids">A collection of Organization IDs.</param>
    /// <param name="queryOptions">If true, disables change tracking for the returned entities, potentially improving performance.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A list of Organizations matching the provided IDs.  If an ID doesn't correspond to an existing Organization, it is omitted in the result.</returns> 
    ValueTask<IList<Organization>> GetByIdsAsync(IEnumerable<Guid> ids, QueryOptions queryOptions = default , CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new Organization record.
    /// </summary>
    /// <param name="organization">The Organization object to be created.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The newly created Organization object.</returns>
    ValueTask<Organization> CreateAsync(Organization organization, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an existing Organization record.
    /// </summary>
    /// <param name="organization">The Organization object containing modified data.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The updated Organization object.</returns> 
    ValueTask<Organization> UpdateAsync(Organization organization, CommandOptions commandOptions = default , CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an existing Organization record.
    /// </summary>
    /// <param name="organization">The Organization object to be deleted.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Organization object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    ValueTask<Organization?> DeleteAsync(Organization organization, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an existing Organization record based on its unique identifier (ID).
    /// </summary>
    /// <param name="organizationId">The ID of the Organization to be deleted.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Organization object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    ValueTask<Organization?> DeleteByIdAsync(Guid organizationId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}