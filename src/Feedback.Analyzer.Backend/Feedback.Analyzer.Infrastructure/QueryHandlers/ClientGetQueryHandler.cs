using AutoMapper;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.Clients.Queries;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.QueryHandlers;

/// <summary>
/// Command handler for retrieving a collection of clients based on specified criteria.
/// </summary>
public class ClientGetQueryHandler(IClientService clientService, IMapper mapper) : IQueryHandler<ClientGetQuery, ICollection<ClientDto>>
{
    public async Task<ICollection<ClientDto>> Handle(ClientGetQuery request, CancellationToken cancellationToken)
    {
        var matchedClients =  await clientService.Get(request.ClientFilter, new QueryOptions() { AsNoTracking = true }).ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<ClientDto>>(matchedClients);
    }
}