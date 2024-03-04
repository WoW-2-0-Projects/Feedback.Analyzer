using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.Clients.CommandHandlers;

/// <summary>
/// Commands handler for deleting a client by their unique identifier.
/// </summary>
public class ClientDeleteByIdCommandHandler(IClientService clientService) : ICommandHandler<ClientDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(ClientDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await clientService.DeleteByIdAsync(request.ClientId, cancellationToken: cancellationToken);
        return true;
    }
}