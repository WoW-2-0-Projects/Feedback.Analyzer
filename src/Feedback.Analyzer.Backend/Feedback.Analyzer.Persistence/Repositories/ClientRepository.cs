using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

public class ClientRepository(AppDbContext dbContext) : EntityRepositoryBase<Client, AppDbContext>(dbContext), IClientRepository
{
    public new IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<Client?> GetByIdAsync(Guid clientId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(clientId, asNoTracking, cancellationToken);
    
    public new ValueTask<Client> CreateAsync(Client client, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(client, saveChanges, cancellationToken);

    public new ValueTask<Client> UpdateAsync(Client client, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.UpdateAsync(client, saveChanges, cancellationToken);

    public new ValueTask<Client?> DeleteAsync(Client client, bool saveChanges, CancellationToken cancellationToken = default)
        => base.DeleteAsync(client, saveChanges, cancellationToken);
    
    public new ValueTask<Client?> DeleteByIdAsync(Guid clientId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(clientId, saveChanges, cancellationToken);
}