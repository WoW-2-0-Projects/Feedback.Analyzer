using System.Linq.Expressions;
using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Organizations.Services;

/// <summary>
/// Defines a contract for services that interact with Organization data.
/// </summary>
public interface IOrganizationService
{
    /// <summary>
    /// Retrieves a queryable collection of Organizations, optionally filtered by a predicate.
    /// </summary>
    /// <param name="predicate">An optional expression to filter the retrieved Organizations.</param>
    /// <param name="queryOptions">If true, disables change tracking for the returned entities, potentially improving performance.</param>
    /// <returns>An IQueryable&lt;Organization&gt; that allows for further LINQ-based querying and filtering.</returns>
    IQueryable<Organization> Get(Expression<Func<Organization, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a filtered and potentially paginated queryable collection of organizations.
    /// </summary>
    /// <param name="organizationFilter">The criteria used to filter the results.</param>
    /// <param name="queryOptions">Optional parameters to configure pagination, sorting, etc. (default is no modification).</param>
    /// <returns>An IQueryable of <see cref="Organization"/> instances matching the specified filter and query options.</returns>
    IQueryable<Organization> Get(OrganizationFilter organizationFilter, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves an Organization by its unique identifier (ID) asynchronously.
    /// </summary>
    /// <param name="organizationId">The unique ID of the Organization.</param>
    /// <param name="queryOptions">If true, disables change tracking for the returned entity, potentially improving performance.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A ValueTask&lt;Organization?&gt; representing the asynchronous operation. The result will be the requested Organization if found, otherwise null.</returns>
    ValueTask<Organization?> GetByIdAsync(Guid organizationId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default
    );
    
    /// <summary>
    /// Creates a new Organization record asynchronously.
    /// </summary>
    /// <param name="organization">The Organization object to be created.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. Otherwise, allows for more changes before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The newly created Organization object.</returns>
    /// <exception cref="ArgumentException">Thrown if the provided 'organization' has a default (empty) ID or if an organization with the same ID already exists.</exception>
    public ValueTask<Organization> CreateAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates an existing Organization record asynchronously.
    /// </summary>
    /// <param name="organization">The Organization object containing the modified data.</param>
    /// <param name="commandOptions">Controls whether changes are immediately persisted to the data store.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The updated Organization object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided 'organization' is null.</exception>

    ValueTask<Organization> UpdateAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes an existing Organization record asynchronously, based on the provided Organization object.
    /// </summary>
    /// <param name="organization">The Organization object to be deleted.</param>
    /// <param name="commandOptions">Controls whether changes are immediately persisted to the data store.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Organization object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    ValueTask<Organization?> DeleteAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes an existing Organization record asynchronously, based on its unique identifier (ID).
    /// </summary>
    /// <param name="organizationId">The ID of the Organization to be deleted.</param>
    /// <param name="commandOptions">Controls whether changes are immediately persisted to the data store.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Organization object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    ValueTask<Organization?> DeleteByIdAsync(
        Guid organizationId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
}