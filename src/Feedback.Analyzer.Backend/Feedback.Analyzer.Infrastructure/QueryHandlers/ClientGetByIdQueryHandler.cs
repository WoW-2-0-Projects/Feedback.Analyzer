using Feedback.Analyzer.Application.Clients.Queries;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.QueryHandlers;

/// <summary>
/// Command handler for retrieving a client by their unique identifier.
/// </summary>
public class ClientGetByIdQueryHandler(IClientService clientService) : IQueryHandler<ClientGetByIdQuery, Client>
{
    public async Task<Client> Handle(ClientGetByIdQuery request, CancellationToken cancellationToken)
    {
        return (await clientService.GetByIdAsync(request.ClientId, new QueryOptions() { AsNoTracking = true }, cancellationToken))!;
    }
}