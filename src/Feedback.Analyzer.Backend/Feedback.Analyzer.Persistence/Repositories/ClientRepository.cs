using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing client entities.
/// </summary>
public class ClientRepository(AppDbContext dbContext) : EntityRepositoryBase<Client, AppDbContext>(dbContext), IClientRepository
{
    public new IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, QueryOptions queryOptions = new())
        => base.Get(predicate, queryOptions);

    public new ValueTask<Client?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = new(), CancellationToken cancellationToken = default)
        => base.GetByIdAsync(clientId, queryOptions, cancellationToken);
    
    public new ValueTask<Client> CreateAsync(Client client, CommandOptions commandOptions = new(), CancellationToken cancellationToken = default)
        => base.CreateAsync(client, commandOptions , cancellationToken);

    public new ValueTask<Client> UpdateAsync(Client client, CommandOptions commandOptions = new(), CancellationToken cancellationToken = default)
        => base.UpdateAsync(client, commandOptions, cancellationToken);

    public new ValueTask<Client?> DeleteAsync(Client client, CommandOptions commandOptions = new() , CancellationToken cancellationToken = default)
        => base.DeleteAsync(client, commandOptions, cancellationToken);
    
    public new ValueTask<Client?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = new(), CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(clientId, commandOptions, cancellationToken);
}