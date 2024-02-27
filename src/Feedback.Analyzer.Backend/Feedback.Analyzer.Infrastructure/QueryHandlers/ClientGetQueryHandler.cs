using Feedback.Analyzer.Application.Clients.Queries;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.QueryHandlers;

/// <summary>
/// Command handler for retrieving a collection of clients based on specified criteria.
/// </summary>
public class ClientGetQueryHandler(IClientService clientService) : IQueryHandler<ClientGetQuery, ICollection<Client>>
{
    public async Task<ICollection<Client>> Handle(ClientGetQuery request, CancellationToken cancellationToken)
    {
        return await clientService.Get(request.ClientFilter, new QueryOptions() { AsNoTracking = true }).ToListAsync(cancellationToken);
    }
}