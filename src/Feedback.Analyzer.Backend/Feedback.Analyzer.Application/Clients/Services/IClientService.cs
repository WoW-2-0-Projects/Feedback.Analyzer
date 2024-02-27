using System.Linq.Expressions;
using Feedback.Analyzer.Application.Clients.Model;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Services;

public interface IClientService
{
    IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, QueryOptions queryOptions = default);

    IQueryable<Client> Get(ClientFilter clientFilter, QueryOptions queryOptions = default);
    
    ValueTask<Client?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    ValueTask<Client> CreateAsync(Client client,  CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    ValueTask<Client?> UpdateAsync(Client client,  CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    ValueTask<Client?> DeleteAsync(Client client,  CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    ValueTask<Client?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

}