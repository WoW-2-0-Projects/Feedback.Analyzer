using AutoMapper;
using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.CommandHandlers;

/// <summary>
/// Command handler for updating a client entity.
/// </summary>
public class ClientUpdateCommandHandler(IClientService clientService, IMapper mapper) : ICommandHandler<ClientUpdateCommand, ClientDto>
{
    public async Task<ClientDto> Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
    {
        var client = mapper.Map<Client>(request.Client); 
        var updatedClient = await clientService.UpdateAsync(client, new CommandOptions(), cancellationToken);
        
        return mapper.Map<ClientDto>(updatedClient);
    }
}