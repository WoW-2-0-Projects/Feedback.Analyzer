using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

public class OrganizationRepository(AppDbContext dbContext) 
    : EntityRepositoryBase
        <Organization, AppDbContext>(
        dbContext
    ), IOrganizationRepository
{
    public new IQueryable<Organization> Get(Expression<Func<Organization, bool>>? predicate, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
    
    public new ValueTask<Organization?> GetByIdAsync(Guid organizationId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(organizationId, asNoTracking, cancellationToken);

    public new ValueTask<IList<Organization>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public new ValueTask<Organization> CreateAsync(Organization organization, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(organization, saveChanges, cancellationToken);

    public new ValueTask<Organization> UpdateAsync(Organization organization, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.UpdateAsync(organization, saveChanges, cancellationToken);

    public new ValueTask<Organization?> DeleteAsync(Organization organization, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteAsync(organization, saveChanges, cancellationToken);

    public new ValueTask<Organization?> DeleteByIdAsync(Guid organizationId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(organizationId, saveChanges, cancellationToken);
}