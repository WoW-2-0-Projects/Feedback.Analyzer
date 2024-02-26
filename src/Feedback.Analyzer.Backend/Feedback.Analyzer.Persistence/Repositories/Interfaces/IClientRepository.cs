using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

public interface IClientRepository
{
    IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<Client?> GetByIdAsync(Guid clientId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<Client> CreateAsync(Client client, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Client> UpdateAsync(Client client, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Client?> DeleteAsync(Client client, bool saveChanges, CancellationToken cancellationToken = default);
    
    ValueTask<Client?> DeleteByIdAsync(Guid clientId, bool saveChanges = true, CancellationToken cancellationToken = default);
}