using AutoMapper;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.Clients.Queries;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.QueryHandlers;

/// <summary>
/// Command handler for retrieving a client by their unique identifier.
/// </summary>
public class ClientGetByIdQueryHandler(IClientService clientService, IMapper mapper) : IQueryHandler<ClientGetByIdQuery, ClientDto?>
{
    public async Task<ClientDto?> Handle(ClientGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundClient = await clientService.GetByIdAsync(
            request.ClientId,
            new QueryOptions()
            {
                AsNoTracking = true
            },
            cancellationToken
        );
        
        return mapper.Map<ClientDto>(foundClient);
    }
}