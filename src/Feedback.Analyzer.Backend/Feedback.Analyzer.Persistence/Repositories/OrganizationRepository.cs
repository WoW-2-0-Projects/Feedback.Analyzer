using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Provides data access functionality for Organization entities, 
/// inheriting core repository logic from EntityRepositoryBase.
/// </summary>
public class OrganizationRepository(AppDbContext dbContext) 
    : EntityRepositoryBase
        <Organization, AppDbContext>(
        dbContext
    ), IOrganizationRepository
{
    public new IQueryable<Organization> Get(Expression<Func<Organization, bool>>? predicate, QueryOptions queryOptions = default)
        => base.Get(predicate, queryOptions);
    
    public new ValueTask<Organization?> GetByIdAsync(Guid organizationId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(organizationId, queryOptions, cancellationToken);

    public new ValueTask<IList<Organization>> GetByIdsAsync(IEnumerable<Guid> ids, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        => base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public new ValueTask<Organization> CreateAsync(Organization organization, CommandOptions commandOptions , CancellationToken cancellationToken = default)
        => base.CreateAsync(organization, commandOptions, cancellationToken);

    public new ValueTask<Organization> UpdateAsync(Organization organization, CommandOptions commandOptions, CancellationToken cancellationToken = default)
        => base.UpdateAsync(organization, commandOptions, cancellationToken);

    public new ValueTask<Organization?> DeleteAsync(Organization organization, CommandOptions commandOptions, CancellationToken cancellationToken = default)
        => base.DeleteAsync(organization, commandOptions, cancellationToken);

    public new ValueTask<Organization?> DeleteByIdAsync(Guid organizationId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(organizationId, commandOptions, cancellationToken);
}