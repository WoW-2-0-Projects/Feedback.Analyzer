using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.CommandHandlers;

/// <summary>
/// Command handler for deleting a client by their unique identifier.
/// </summary>
public class ClientDeleteByIdCommandHandler(IClientService clientService) : ICommandHandler<ClientDeleteByIdCommand, Client?>
{
    public async Task<Client?> Handle(ClientDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        return await clientService.DeleteByIdAsync(request.ClientId, new CommandOptions(), cancellationToken: cancellationToken);
    }
}